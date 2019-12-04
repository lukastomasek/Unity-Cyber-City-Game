using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class UniversalHealthController : MonoBehaviour
{
    public float health = 100f;
    [SerializeField]
    private float _stunned = 0.4f;

    [Header("This will disable players movement after hit")]
    public float disabledMovementTimer = 1f;

    #region BOOLS
   
    public bool isPlayer, isEnemy, isViper;
    [HideInInspector]
    public bool _is_Dead;
    private bool _isBlocking;
    private bool _isHit;
    [HideInInspector]
    public bool isKnockedOut;
    [Header("TESTING")]
    public bool hasPlayed;

    [Header("Only Count enemies and enable you win text ")]
    public bool isFinalEnemy;
    #endregion  

    public Image healthUI;
   

    private CharacterAnimations _animations;
    private CharacterAttackController _playerController;
    private CharacterMovement _playerMovement;
    private ShootingController _shootingController;
    private EnemyController _enemyController;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;
    private AudioSource _enemyAudio;
    private TheViperController _viperController;
    private EnemySpawner enemySpawner;


    public delegate void onPlayerDeadEvent();
    public static event onPlayerDeadEvent showYouDiedPanel;


    private void Start()
    {
        // if user press load button then we this will make sure to load him to the check point 
        if (GameManager.instance.canLoadPlayerData)
            LoadPlayer();
    }


    void Awake()
    {
       

        if (isPlayer)
        {
            _playerController = GetComponent<CharacterAttackController>();
            _playerMovement = GetComponent<CharacterMovement>();
            _rigidbody = GetComponent<Rigidbody>();
            _shootingController = GetComponent<ShootingController>();
        }
        if (isEnemy)
        {
            _enemyController = GetComponent<EnemyController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            
          
        }

        if (isViper)
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyAudio = GetComponent<AudioSource>();
            _viperController = GetComponent<TheViperController>();
        }

      


        _animations = GetComponent<CharacterAnimations>();
      
       
    }


    void Update()
    {
        //Stunned(); 
      
    }

    public void ApplyDamage( float damage)
    {
        if (_is_Dead || _isBlocking)
            return;

        health -= damage;


       if(healthUI != null)
       {
            healthUI.fillAmount = health / 100f;
       }


        if (health <= 0)
        {
            _is_Dead = true;
            CharacterDied();
            
        }
        else
        {
            if (isPlayer)
            {
                ShakeCameraAfterHit.enableShakeFX.TurnOnShakeScript();
                _animations.RegularHit();
                StartCoroutine(PlayerHit());
            }
            else
            {
                _animations.RegularHit();
                _isHit = true;
                isKnockedOut = true;
            }


          
          
        }

    } // apply damage 


    void CharacterDied()
    {
        if (isPlayer)
        {
            Debug.Log("Player Died!");
            _animations.DeadAnims(Random.Range(0, 2));
            _playerController.enabled = false;
            _playerMovement.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            _shootingController.enabled = false;
           // GameManager.instance.RestartGame();

            StartCoroutine(PlayerDeadEvent());
        }

        if (isEnemy || isFinalEnemy)
        {
            _animations.StopAllanimations();
            _animations.DeadAnims(Random.Range(0,2));
            _enemyController.enabled = false;           
            _navMeshAgent.enabled = false;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Enemy Died" + gameObject.name);

            // add player exp
            UnlockCombos.instance.myexp += 5;

            //if (isFinalEnemy)
            //{
            //    finalEnemyDeadCheck.FinalEnemyIsDead();
            //}
          
            
        }

        if (isViper)
        {
            _animations.StopAllanimations();
            _animations.ViperDeadAnim();
            _viperController.enabled = false;
            _navMeshAgent.enabled = false;
            _enemyAudio.enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            enabled = false;

            // add player exp
            UnlockCombos.instance.myexp += 10;
        }


       
    }

    #region STUNNED FUNCTIONS

    public void Stunned()
    {
        if (!_is_Dead)
        {
            StartCoroutine("WaitAfterStunned");
        }
    }


    IEnumerator WaitAfterStunned()
    {

        if (isEnemy && _isHit)
        {
            _isHit = false;
            //  Debug.LogWarning("Enemy Stunned");
            _enemyController.enabled = false;
            _navMeshAgent.enabled = false;
            yield return new WaitForSecondsRealtime(_stunned);
            _navMeshAgent.enabled = true;
            _enemyController.enabled = true;
        }
        if (isPlayer && _isHit)
        {
            _isHit = false;
            // Debug.LogWarning("Player Stunned");
            _playerController.enabled = false;
            _playerMovement.enabled = false;
            yield return new WaitForSecondsRealtime(_stunned);
            _playerController.enabled = true;
            _playerMovement.enabled = true;
        }

    }

    #endregion


    private IEnumerator PlayerHit()
    {
        _playerMovement.enabled = false;
        yield return new WaitForSeconds(disabledMovementTimer);
        _playerMovement.enabled = true;

    }

    private IEnumerator PlayerDeadEvent()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Event died enabled");
        showYouDiedPanel?.Invoke();
    }

    public void DisableEnemyScriptsAfterKnockedOut()
    {
        StartCoroutine(DisableEnemy());

    }

    private IEnumerator DisableEnemy()
    {
        if (isEnemy)
        {
            StopCoroutine("WaitAfterStunned");
            //Debug.Log("Disable Enemy Called");
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            collider.enabled = false;
            _enemyController.enabled = false;
            yield return new WaitForSecondsRealtime(8f);
            _enemyController.enabled = true;
            collider.enabled = true;
            if (!_navMeshAgent.enabled)
                _navMeshAgent.enabled = true;

        }
    }

    void EnemyKnocked()
    {
        _animations.StopAllanimations();
        if (isEnemy)
        {
            _animations.KnockedOutAnim();
            DisableEnemyScriptsAfterKnockedOut();
        }

        // Debug.Log("Enemy Knocked Out");
        // try to resolve null reference on combo finished or remove delegate and call it like normal function

        if (isPlayer)
        {
            _playerController.ComboFinished = false;
        }
        isKnockedOut = false;
    }


    public bool Blocking
    {
        get { return _isBlocking; }
        set { _isBlocking = value; }
    }


    private void OnEnable()
    {
        DelegationController.EnemyIsKnocked += EnemyKnocked;
    }

    private void OnDisable()
    {
        DelegationController.EnemyIsKnocked -= EnemyKnocked;
    }



    #region SAVING AND LOADING THE PLAYER DATA

    //saving player health and position 

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

        
    }

    // loading player data 
    public void LoadPlayer()
    {
      PlayerData data =  SaveSystem.LoadPlayer();

        if (isPlayer)
        {
            health = data.health;
            healthUI.fillAmount = health / 100f;
          

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            transform.position = position;
        }
                

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoint1"))
        {
            SavePlayer();
        }
    }

    #endregion




} // end

