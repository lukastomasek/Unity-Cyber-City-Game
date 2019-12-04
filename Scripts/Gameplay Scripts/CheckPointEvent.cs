using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPointEvent : MonoBehaviour
{
    private GameplayManager gameplayManager;
    private UniversalHealthController healthController;
    private MissionManagerUI missionUI;
    private GameManager gameManager;
    private MissionManagerUI managerUI;

    private void Awake()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
        healthController = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<UniversalHealthController>();
        gameManager = FindObjectOfType<GameManager>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            // if we load data this will make sure to play the cutscene in gameplay manager
            // without saving the states of the game 
            if(gameplayManager.checkPointCompleted != true)
            {
                gameplayManager.checkPointCompleted = true;

            }
          

            gameManager.hasSavedFirstTime = true;

            gameManager.SavehasPlayedFirstTime();
            Debug.Log($"value of has saved time : {gameManager.hasSavedFirstTime}");

            gameplayManager.SpawnCookers(4);

            gameObject.SetActive(false);
            
           
        }
    }
}
