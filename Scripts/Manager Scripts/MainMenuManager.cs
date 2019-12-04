using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Buttons
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button loadButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private Button closeCreditBtn;

    #endregion


    #region Panels
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private GameObject creditsPanel;
    #endregion

    private string levelName = "Gameplay";

    private GameManager gameManager;

    private void Awake()
    {
        //   gameManager = GameManager.instance;
        gameManager = FindObjectOfType<GameManager>();
    }


    private void Start()
    {
        startButton.onClick.AddListener(() => StartNewGame());
        loadButton.onClick.AddListener(() => LoadGame());
        quitButton.onClick.AddListener(() => QuitGame());
        optionsButton.onClick.AddListener(() => EnableOptions());
        closeButton.onClick.AddListener(() => DisableOptions());
        creditsButton.onClick.AddListener(() => EnableCreditsPanel());
        closeCreditBtn.onClick.AddListener(() => DisableCreditsPanel());

        gameManager.gameState = GameState.PREGAME;
    }



    private void StartNewGame()
    {
        gameManager.LoadLevel("Gameplay");
    }


    private void LoadGame()
    {
        gameManager.LoadLevelWithSaving(levelName);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application succesfully closed !");
    }


    private void EnableOptions()
    {
        if(!optionsPanel.activeInHierarchy)
        optionsPanel.SetActive(true);
    }

    private void DisableOptions()
    {
        if (optionsPanel.activeInHierarchy)
            optionsPanel.SetActive(false);
    }

    private void EnableCreditsPanel()
    {
        if (!creditsPanel.activeInHierarchy)
            creditsPanel.SetActive(true);
    }

    private void DisableCreditsPanel()
    {
        if (creditsPanel.activeInHierarchy)
            creditsPanel.SetActive(false);
    }
}
