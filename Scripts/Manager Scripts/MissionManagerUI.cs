using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  inplemented in gameply manager, itemineraction and checkpoint event 
/// </summary>

public class MissionManagerUI : MonoBehaviour
{
    public static MissionManagerUI instance;
    
 
    #region Singelton
    private void Awake() => MakeSingelton();

    private void MakeSingelton()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public Text missionTxt1;
    public Text missionTxt2;
    public Text missionTxt3;
    public Text missionTxt4;


    public Image boxImg;
    public Image checkMark;


    public CanvasGroup savingTxt;
    public GameObject youWonPanel;
    public Button BackToMainMnBtn;
    public Button RestartBtn;
    #region Mission UI Texts

    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;

        BackToMainMnBtn.onClick.AddListener(() => gameManager.BackToMainMenu());
        RestartBtn.onClick.AddListener(() => gameManager.ResetGame());
    }



    public void ShowKillEnemiesUI()
    {
        Debug.Log("Mission UI is updating");
        if (!missionTxt1.enabled)
        {
            missionTxt1.enabled = true;
           
           
        }
        missionTxt1.color = Color.red;
        checkMark.enabled = true;
       

        Invoke("EnableSecondUI", 2f);


    }


    private void EnableSecondUI()
    {
        missionTxt1.enabled = false;
        missionTxt2.enabled = true;
        checkMark.enabled = false;
    }


    public void ShowUnlockGateTxt()
    {
        missionTxt2.color = Color.red;
        checkMark.enabled = true;

        Invoke("EnableThirdUI", 2f);
    }


    private void EnableThirdUI()
    {
        missionTxt2.enabled = false;
        missionTxt3.enabled = true;
        checkMark.enabled = false;
    }

    public void ShowKillLastStandingEnemiesTxt()
    {
        missionTxt3.color = Color.red;
        checkMark.enabled = true;

        Invoke("EnableFourthUI", 2f);
    }

    private void EnableFourthUI()
    {
        missionTxt3.enabled = false;
        missionTxt4.enabled = true;
        checkMark.enabled = false;
    }

    #endregion



    public void ShowSavingText()
    {
        StartCoroutine(EnableSavingTxtCourutine());
    }


    private IEnumerator EnableSavingTxtCourutine()
    {
        savingTxt.alpha = 1;
        yield return new WaitForSeconds(3f);
        savingTxt.alpha = 0;
    }


    public void EnableYouWonPanel()
    {
        gameManager.gameState = GameState.PAUSED;
        youWonPanel.SetActive(true);
       
    }

  
}
