using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;



public enum GameState
{
    PREGAME,
    RUNNING,
    PAUSED
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState { get; set; }

    [HideInInspector]
    public bool canLoadPlayerData;

    public  bool hasSavedFirstTime { get; set; } 

    private string currentLevel;

    

    #region Buttons
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private Button continueBtn;

    [SerializeField]
    private Button restartBtn;

    [SerializeField]
    private Button quitBtn;
    #endregion

    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private Image loadingBar;

    #region Singelton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more game manager instances in the scene !");
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion


    private void Start()
    {
        
        continueBtn.onClick.AddListener(() => ContinueGame());
        restartBtn.onClick.AddListener(() => ResetGame());
        quitBtn.onClick.AddListener(() => BackToMainMenu());

        EnablePlayerToLoadGame();
    }


  

    

    private void Update()
    {
        
        SwitchGameState();
        OnPauseState();
    }


    public void SwitchGameState()
    {
        switch (gameState)
        {
            case GameState.PREGAME:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f;
                break;

            case GameState.RUNNING:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                break;

            case GameState.PAUSED:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                break;

            default:
                gameState = GameState.RUNNING;
                break;
        }

    }


    private void ContinueGame()
    {
        gameState = GameState.RUNNING;
        pausePanel.SetActive(false);
    }

    public void ResetGame()
    {
       LoadLevel("Gameplay");
       pausePanel.SetActive(false);
       
    }

    public void BackToMainMenu()
    {
        gameState = GameState.PREGAME;
         LoadLevel("MainMenu");
      
        pausePanel.SetActive(false);
        
    }

    public void OnPauseState()
    {

        if (gameState == GameState.PREGAME)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.PAUSED;
            pausePanel.active = !pausePanel.active;
            Debug.Log("game state :" + gameState);

            if (!pausePanel.activeInHierarchy)
                gameState = GameState.RUNNING;
        } 
     
    }

    public void LoadLevel2()
    {
        LoadingScreen.instance.ShowLoadingScreen("Gameplay");
        LoadingLevelComplete();
    }
   
    public void LoadLevel(string LevelName)
    {
        LoadingScreen.instance.ShowLoadingScreen(LevelName);
        currentLevel = LevelName;
        Debug.Log($"level loaded : {currentLevel}");
        LoadingLevelComplete();

        #region saving
        //AsyncOperation ao = SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Single);
        //loadingPanel.SetActive(true);
        //Debug.Log($"level loaded{LevelName}");
        //currentLevel = LevelName;

        //if (ao == null)
        //{
        //    Debug.LogError("Unable To load level due to incorrect level name");
        //    return;
        //}
        //LoadingLevelComplete();
        #endregion
    }

    private IEnumerator ShowLoadingScreen( string LevelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Single);
        loadingPanel.SetActive(true);
        Debug.Log($"level loaded{LevelName}");
        currentLevel = LevelName;

        if (ao == null)
        {
            Debug.LogError("Unable To load level due to incorrect level name");
           
        }
        while (!ao.isDone)
        {
            float progress = ao.progress / 0.9f;
            loadingBar.fillAmount = progress;

            if(progress >= 1)
            {
                loadingPanel.SetActive(false);
            }

            yield return null;
        }

        LoadingLevelComplete();
    }

    public void LoadLevelWithSaving(string levelName)
    {
        if (hasSavedFirstTime == true)
        {
            LoadingScreen.instance.ShowLoadingScreen(levelName);
            currentLevel = levelName;
            Debug.Log($"level loaded : {currentLevel}");
            canLoadPlayerData = true;
            LoadingLevelComplete();
        }
        else if(hasSavedFirstTime == false)
        {
            Debug.LogError("Nothing was saved");
            return;
        }
        #region Loading without loading screen
        //if (hasSavedFirstTime == false)
        //{
        //    Debug.Log("There is no save data");
        //    return;
        //}

        //AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

        //currentLevel = levelName;
        //canLoadPlayerData = true;
        //Debug.Log($"level loaded : {levelName}");

        //if (ao == null)
        //{
        //    Debug.LogError("Unable to load level due to incorrect level name or invalid saving file..");
        //    return;
        //}


        //LoadingLevelComplete();
        #endregion
    }


    private IEnumerator ShowLoadingScreenWithSaving( string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        loadingPanel.SetActive(true);
        currentLevel = levelName;
        canLoadPlayerData = true;
        Debug.Log($"level loaded : {levelName}");

        if (ao == null)
        {
            Debug.LogError("Unable to load level due to incorrect level name or invalid saving file..");
           
        }

        while (!ao.isDone)
        {
            float progress = ao.progress / 0.9f;
            loadingBar.fillAmount = progress;

            if(progress >= 1)
            {
                loadingPanel.SetActive(false);
            }

            yield return null;
        }

        LoadingLevelComplete();
    }
    private void LoadingLevelComplete()
    {
      
        gameState = GameState.RUNNING;
    }


    public void QuitApplication(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

            if (ao.isDone)
            {
                Application.Quit();

                Debug.Log("Application succesfully closed !");
            }
        
       
    }


    #region Save Bool Has saved first time

    public void SavehasPlayedFirstTime()
    {
        Debug.Log($"saving bool has played first time!");
        PlayerPrefs.SetInt("hasPlayedFirstTime", (hasSavedFirstTime ? 1 : 0));
    }
  
    private void EnablePlayerToLoadGame()
    {
        Debug.Log("loading bool value for has saved first time");
        hasSavedFirstTime = (PlayerPrefs.GetInt("hasPlayedFirstTime") != 0);
    }

    #endregion


}

