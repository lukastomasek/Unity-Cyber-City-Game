using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyAttack { NONE,PATROL ,CHASE, ATTACK, SHOOT}

[RequireComponent(typeof(NavMeshAgent))]
public class TheViperController : MonoBehaviour
{
    #region Values

    [SerializeField][Range(2f,15f)]
    private float patrolRadius = 6f;

    [SerializeField][Range(1.5f, 10.5f)]
    private float patrolTimer = 3.1f;

    [SerializeField][Range(1.1f, 2.2f)]
    private float walk = 1.2f;

    [SerializeField][Range(2.2f, 3.1f)]
    private float run = 2.2f;

    [SerializeField][Range(1.5f, 3.5f)]
    private float waitBeforeAttack = 0.5f;

    [SerializeField][Range(1,5)]
    private float attackDistance = 3f;

    [SerializeField][Range(3,11)]
    private float shootRadius = 6f;

    [SerializeField][Range(5,10)]
    private float chaseDistance = 5f;

    [SerializeField][Range(0.5f, 3.2f)]
    private float fireRate = 1f;

    [SerializeField][Range(1, 10)]
    private int shootDamage = 5;

    #endregion

   
    public Transform firePoint;
    public Transform muzzlePoint;
    public GameObject muzzleEffect;
    public AudioClip shootClip;
    public List<AudioClip> enemyWarnings = new List<AudioClip>();

    private float currentPatrolTimer;
    private float currentShootTimer;
    private float attackTimer;

    private CharacterAnimations anim;
    private NavMeshAgent agent;
    private EnemyAttack enemyAttack;
    private AudioSource audioSource;
    private Transform targetPos;
    private UniversalHealthController healthController;
    private GameplayManager gameplayManager;

    [Header("This is for informing  a game manager to enable kaycard after all vipers are dead")]
    private int enemyCount;
    public bool enableKeycard = false;

    private void Awake()
    {
        anim = GetComponent<CharacterAnimations>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        targetPos = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        healthController = GetComponent<UniversalHealthController>();
        gameplayManager = FindObjectOfType<GameplayManager>();
    }


    private void Start()
    {
        enemyAttack = EnemyAttack.PATROL;
    }


    private void Update()
    {
        if(enemyAttack == EnemyAttack.PATROL)
        {
            Patrol();
        }

        else if(enemyAttack == EnemyAttack.CHASE)
        {
            Chase();
        }
        else if(enemyAttack == EnemyAttack.ATTACK)
        {
            Attack();       
        }
        else if(enemyAttack == EnemyAttack.SHOOT)
        {
            Shoot();
        }


      

    }



    #region Patrol And Chase

    private void Patrol()
    {
        agent.isStopped = false;
        agent.speed = walk;

        currentPatrolTimer += Time.deltaTime;

        if (currentPatrolTimer >= patrolTimer)
        {
            currentPatrolTimer = 0f;
            SetNewDestination();
        }


        if(agent.velocity.sqrMagnitude > 0)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);
        }

        if(Vector3.Distance(transform.position, targetPos.position) <= chaseDistance)
        {
            enemyAttack = EnemyAttack.CHASE;
        }
        else if (Vector3.Distance(transform.position, targetPos.position) > chaseDistance)
        {
            enemyAttack = EnemyAttack.PATROL;       
        }
       

    }


    private void SetNewDestination()
    {
        Vector3 newDestination = NewDestinationInSphere(transform.position, patrolRadius, -1);
        agent.SetDestination(newDestination);
    }


    private Vector3 NewDestinationInSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * distance;
        randDir += origin;

        NavMeshHit hit;

        NavMesh.SamplePosition(randDir, out hit, distance, layerMask);

        return hit.position;
    }

   


    private void Chase()
    {
        agent.SetDestination(targetPos.position);
        agent.isStopped = false;
        agent.speed = run;

        if(agent.velocity.sqrMagnitude > 0)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);
        }

        if(Vector3.Distance(transform.position, targetPos.position) <= attackDistance 
            && agent.remainingDistance <= 2f)
        {
            enemyAttack = EnemyAttack.ATTACK;
        } 

     
      
    }

    #endregion

    #region Attacking And Shooting

    private void Attack()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        anim.Walk(false);

        attackTimer += Time.deltaTime;

        if(attackTimer > waitBeforeAttack)
        {
            agent.transform.LookAt(targetPos.position);

            if(Random.Range(0, 4)> 1)
            {
                if(Random.Range(0,3)> 1)
                {
                    anim.Punch1();
                }
                else
                {
                   if(Random.Range(0,3) < 1)
                    {
                        anim.Punch2();
                    }
                    else
                    {
                        anim.Punch3();
                    }
                }
            }
            else
            {
                anim.QuakeKick();
            }
            attackTimer = 0f;
        }


        if(Vector3.Distance(transform.position, targetPos.position) > attackDistance && 
            Vector3.Distance(transform.position, targetPos.position)< shootRadius)
        {
            enemyAttack = EnemyAttack.SHOOT;
        }

        else if(Vector3.Distance(transform.position , targetPos.position) > shootRadius)
        {
            enemyAttack = EnemyAttack.CHASE;
        }
        
    }



    private void Shoot()
    {
        agent.isStopped = true;
        anim.Walk(false);

        Ray shootRay = new Ray( firePoint.position, firePoint.forward);
        RaycastHit infoHit;
        agent.transform.LookAt(targetPos.position);
       
        Debug.DrawRay(firePoint.position, firePoint.forward * 100f, Color.blue, 2f);
        
        currentShootTimer += Time.deltaTime;

        float randomFireRate = Random.Range(2f, 4f);

        if (currentShootTimer > randomFireRate + 1)
        {
            currentShootTimer = 0f;
            Invoke("PlayShootAnim", 0.3f);

           
            var plasmaMuzzle = Instantiate(muzzleEffect, muzzlePoint.position, Quaternion.identity);

            if (Physics.Raycast(shootRay, out infoHit, 100f))
            {
                //Debug.Log("Enemy Shot " + infoHit.collider.name);

                if (infoHit.collider.gameObject.CompareTag(Tags.PLAYER))
                {
                    UniversalHealthController enemyhealth = infoHit.collider.gameObject.GetComponent<UniversalHealthController>();

                    if (enemyhealth != null)
                    {
                        enemyhealth.ApplyDamage(shootDamage);
                    }
                }
            }
        }


        if(Vector3.Distance(transform.position, targetPos.position) > shootRadius)
        {
            enemyAttack = EnemyAttack.CHASE;
        }
       
    }


    private void PlayShootAnim()
    {
        anim.EnemyShoot();
        audioSource.PlayOneShot(shootClip);
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            int randomWarning = Random.Range(0, enemyWarnings.Count);

            for(int i = 0; i < randomWarning; i++)
            {
                audioSource.clip = enemyWarnings[randomWarning];
                audioSource.PlayDelayed(1.2f);
            }
        }
    }


    public void OnInformViperIsDead()
    {
        if(gameplayManager == null)
        {
            Debug.LogError("missing instance of gameplay manager !");

            return;
        }

        gameplayManager.ViperIsDead();
    }


} // end  
