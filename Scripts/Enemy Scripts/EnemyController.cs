using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



 // TO DO LIST:
 // [x] FIX ANIMATION FOR ENEMIES
 // [x] FIX THE MOVE BACK FROM PLAYER FUNCION
 // [x] CREATE TWO SPEEDS FOR PATROLING AND CHASING
 // [x]add effect for the gun shot
 // [x]add gun shot sound
 // [] fix the effect so its line up with shoot anim


public enum EnemyAttackBehaviour
{
    NONE,  PATROL, CHASE, ATTACK
} 

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float chaseSpeed = 3.5f;

    public float patrolRadius = 6f;
    public float patrolTimer = 3f;
    private float currentPatrolTime;

    public float waitBeforeAttack = 1f;
    private float attackTimer;
    public float waitBeforeChase = 1f;

    public float attackDistance = 3f;
    public float shootDistance = 6f;
    public int chaseDistance = 4;
    public bool isRegularEnemy;

    public GameplayManager gameplayManager;
    private Vector3 moveBackFromPlayer;
    public float retreatDistance = 4f;

    private NavMeshAgent agent;
    private EnemyAttackBehaviour enemyBehaviour;
    
    private CharacterAnimations enemyAnim;

    private Transform PlayerPos;

    


    private void Awake()
    {
        enemyAnim = GetComponent<CharacterAnimations>();
        agent = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        gameplayManager = FindObjectOfType<GameplayManager>(); 
       
    }

    void Start()
    {
        enemyBehaviour = EnemyAttackBehaviour.PATROL;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyBehaviour == EnemyAttackBehaviour.PATROL)
        {
            Patrol();
        }

        if(enemyBehaviour == EnemyAttackBehaviour.CHASE)
        {
            Chase();
        }

        if(enemyBehaviour == EnemyAttackBehaviour.ATTACK)
        {
            Attack();
        }
      

     
        
    } // update


    void Patrol()
    {
        currentPatrolTime += Time.deltaTime;
        agent.speed = walkSpeed;

        #region Walk 
        if (agent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }

        #endregion

        if (currentPatrolTime >= patrolTimer)
        {
            currentPatrolTime = 0f;
            SetNewRandomDestination();
        }

        if(Vector3.Distance(transform.position, PlayerPos.position) <= chaseDistance)
        {
            enemyBehaviour = EnemyAttackBehaviour.CHASE;
        } 
        else if(Vector3.Distance(transform.position, PlayerPos.position) > chaseDistance)
        {
            enemyBehaviour = EnemyAttackBehaviour.PATROL;
        }

       
    }

    private void SetNewRandomDestination()
    {
        Vector3 newDestination = RandomNavSphere(transform.position, patrolRadius, -1);
        agent.SetDestination(newDestination);
    }


    private Vector3 RandomNavSphere(Vector3 originPos, float distance, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * distance;
        randDir += originPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randDir, out hit, distance, layerMask);

        return hit.position;
    }


    void Chase()
    {
        agent.SetDestination(PlayerPos.position);
        agent.speed = walkSpeed;
        agent.isStopped = false;
        if(Vector3.Distance(transform.position, PlayerPos.position) <= attackDistance)
        {
            enemyBehaviour = EnemyAttackBehaviour.ATTACK;
        }

        if(agent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }


    } // chase


    void Attack()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        enemyAnim.Walk(false);

        attackTimer += Time.deltaTime;
        if(attackTimer > waitBeforeAttack)
        {
            transform.LookAt(PlayerPos.position);
           if(Random.Range(0,4) > 1)
            {
                if(Random.Range(0,3)> 1)
                {
                    enemyAnim.Punch1();
                }
                else
                {
                    if(Random.Range(0,3) < 1)
                    {
                        enemyAnim.Punch3();
                    }
                    else
                    {
                        enemyAnim.QuakeKick();
                    }
                }
           }
            else
            {
                enemyAnim.Punch2();     
            }

            attackTimer = 0f;
        }

     
        if(Vector3.Distance(transform.position, PlayerPos.position) > attackDistance + waitBeforeChase)
        {
            enemyBehaviour = EnemyAttackBehaviour.CHASE;
            agent.isStopped = false;
        }
        

    } // attack 

    #region Move Away From Player
    private void MoveBackFromPlayer()
    {
        

        if(Vector3.Distance(transform.position, PlayerPos.position)<= 0.5f)
        {
            moveBackFromPlayer = transform.position - (transform.forward * retreatDistance);
            agent.SetDestination(moveBackFromPlayer);
            
            if(agent.stoppingDistance <= 0.1f)
            {
                StartCoroutine(MoveBackToPlayer());
            }
        }
    }

    private IEnumerator MoveBackToPlayer()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        enemyBehaviour = EnemyAttackBehaviour.ATTACK;
    }

    #endregion

  
 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }


    public void OnInformRegularEnemyIsDead()
    {
        if (isRegularEnemy)
        {
            if (gameplayManager == null)
            {
                Debug.LogWarning("Gameplay Manager is Not Attached On The Script!");
                return;
            }

            Debug.Log("informing gameplay manager: regualar enemy is dead !");
            gameplayManager.EnemyIsDead();
        }
    }


    public void OnFinalEnemyIsDead()
    {
        if(gameplayManager == null)
        {
            Debug.LogWarning("Gameplay Manager is Not Attached On The Script!");
        }

        gameplayManager.FinalEnemyDead();
    }

} // end
