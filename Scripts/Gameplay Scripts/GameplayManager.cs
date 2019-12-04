using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    #region CutScenes
    public ViperCutScene viperCutScene;
    public CookCutScene cookCutScene;
    #endregion

    public GateController gateController;
    public EnemySpawner enemySpawner;
    public GameObject keycard;
    private CharacterMovement playerMovement;
    private UniversalHealthController healthController;
    private MissionManagerUI missionUI;

    public Transform stationPoint;
    private Transform player;

    private bool playerHasKeycard;

    [SerializeField]
    private int enemyCount = 5;

    private int viperCount = 3;

    private int finalEnemyCount = 4;

    [SerializeField]
    private CanvasGroup openGateTxt;

    [SerializeField]
    private CanvasGroup pickUpKeyTxt;


    [SerializeField]
    [TextArea]
    private string purpose;


    [HideInInspector]
    public bool checkPointCompleted;

    #region Buttons

    [SerializeField]
    private GameObject deadPanel;

    [SerializeField]
    private Button restartBtn;

    [SerializeField]
    private Button quitBtn;

    #endregion


    private float currentTimer;
    private float timerBeforeEnablingPlayerMovement = 7.5f;


    #region Singelton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Gameplay Manager instance is not null !");
            return;
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        playerMovement = player.GetComponent<CharacterMovement>();
        healthController = player.GetComponent<UniversalHealthController>();

        // this makes sure that we inform game manager that we are in gameplay scene
        // so we can enable and disable pause menu
        GameManager.instance.gameState = GameState.RUNNING;
        missionUI = MissionManagerUI.instance;
        restartBtn.onClick.AddListener(() => RestartGameOnCheckPoint());
        quitBtn.onClick.AddListener(() => QuitToMainMenu());
    }


    private void OnEnable()
    {
        UniversalHealthController.showYouDiedPanel += EnableDeadPanel;
        
    }


    private void OnDisable()
    {
        UniversalHealthController.showYouDiedPanel -= EnableDeadPanel;
       
    }

    private void Update()
    {
        PlayerisInTheRange();

    }


    public void ViperIsDead()
    {
        viperCount--;

        if (viperCount == 0)
        {
            missionUI.ShowKillEnemiesUI();
            keycard.SetActive(true);
        }
    }

    public void FinalEnemyDead()
    {
        finalEnemyCount--;

        if(finalEnemyCount == 0)
        {
            missionUI.EnableYouWonPanel();
            Debug.Log("all enemies died , YOU WON !");
        }
    }


    private void EnableDeadPanel()
    {
        deadPanel.SetActive(true);
        GameManager.instance.gameState = GameState.PAUSED;

    }

    private void RestartGameOnCheckPoint()
    {
        if (checkPointCompleted)
        {
            GameManager.instance.LoadLevelWithSaving("Gameplay");
            deadPanel.SetActive(false);

            

        }
        else
        {
            GameManager.instance.ResetGame();
        }

    } 

    private void QuitToMainMenu()
    {
        GameManager.instance.BackToMainMenu();
    }

    public void EnemyIsDead()
    {
        enemyCount--;

        if (enemyCount == 0)
        {
            viperCutScene.EnableCutScene1();
            enemySpawner.SpawnVipers(3);
           
            viperCutScene.EnablePlayerMovementAndCamera();
        }
    }

    
     
    public void HasKeyCard(bool hasKey)
    {
        Debug.Log("player picked up a keycard");

        // recieved a message from iteminteraction 
        // which is bool that is returning  true player picked up a keycard
        playerHasKeycard = hasKey;
    }



    private void PlayerisInTheRange()
    {
        // Vector3 direction = player.position - stationPoint.position;

        Ray rangeRay = new Ray(stationPoint.position, stationPoint.forward);
        Debug.DrawRay(stationPoint.position, stationPoint.forward, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(rangeRay, out hit, 15f))
        {
            if (hit.collider.gameObject.CompareTag(Tags.PLAYER))
            {

                if (playerHasKeycard)
                {
                    healthController.hasPlayed = true;
                    openGateTxt.alpha = 1;

                    if (Input.GetKey(KeyCode.E))
                    {
                        missionUI.ShowKillLastStandingEnemiesTxt();
                        checkPointCompleted = true;
                        gateController.UnlockGate();
                        playerHasKeycard = false;
                        StartCoroutine(DisableText());
                        missionUI.ShowSavingText();
                        return;
                    }
                }
                else if (!playerHasKeycard)
                {
                    pickUpKeyTxt.alpha = 1;
                    StartCoroutine(DisableText());

                    return;

                }
            }


        }
    }


    // calling this function in the check point event   
    public void SpawnCookers(int count)
    {
        cookCutScene.PlayCookCutScene2();
        Debug.Log("Check Point Combleted ");
        StartCoroutine(WaitBeforeSpawningCookers(count));
        cookCutScene.EnablePlayerMovementAndCamera();
        StopPlayer();

        currentTimer += Time.deltaTime;
        if (currentTimer >= timerBeforeEnablingPlayerMovement)
            EnablePlayer();
    }


    private void StopPlayer()
    {
        playerMovement.StopPlayer();
       
    }

    private void EnablePlayer()
    {
        playerMovement.EnablePlayer();
    }
   

    private IEnumerator WaitBeforeSpawningCookers(int count)
    {
        yield return new WaitForSecondsRealtime(1f);
        enemySpawner.SpawnDrugCookers(count);

    }


    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(2);
        openGateTxt.alpha = 0f;
        pickUpKeyTxt.alpha = 0;
    }

} // end 
