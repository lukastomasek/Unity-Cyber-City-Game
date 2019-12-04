using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinalEnemyIsDead : MonoBehaviour
{
    [SerializeField]
    private int enemyCount;
    private GameObject[] enemies;
    private UniversalHealthController healthController;
    private bool killedAllEnemies = false;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("FinalEnemy");

        foreach (var enemy in enemies)
        {
            healthController = enemy.gameObject.GetComponent<UniversalHealthController>();
        }


        enemyCount = 4;
        Debug.Log(enemyCount);
    }

    private void Update()
    {

        enemyCount = enemies.Length;

      
        
       if (healthController.isFinalEnemy )
       {
                if(healthController._is_Dead == true)
                enemyCount--;
       }
            
        
      
        if (enemyCount == 0)
        {
            EndGame();
        }


    }


    private void EndGame()
    {
        Debug.Log("all enemies are dead");
        killedAllEnemies = true;
    }
}
