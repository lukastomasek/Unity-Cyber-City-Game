using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCombos : MonoBehaviour
{
    public static UnlockCombos instance;

    #region Singelton
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion 

    public PlayerStats playerStats;

    public int myexp;
    public int myLevel = 0;


    private void Start()
    {
       
        
    }

  

    private void Update()
    {
        CountExp();
    }

    private void CountExp()
    {
        if (playerStats.expRequired <= myexp)
        {
            myLevel = 1;
        }

        if(playerStats.level == myLevel)
        {
            Debug.Log("Able to unlock new skill!");
        }
    }



    public void SavePlayerExp()
    {
        SaveSystem.SavePlayerLevelAndExp(this);
    }

    public void LoadPlayerExp()
    {
        PlayerStats playerStats = SaveSystem.LoadPlayerExpAndLevel();

        myexp = playerStats.expRequired;
        myLevel = playerStats.level;
    }



    private void OnDisable()
    {
        SavePlayerExp();
    }
}
