using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemySpawnPoints;
    public Transform[] gateSpawnPoints;
    public Transform[] buidlingSpawnPoints;
    public Transform[] viperEffectSpawnPoints;

    [SerializeField]
    private GameObject[] _enemyRegularPrefabs;

    [SerializeField]
    private GameObject viperSpawnEffect;

    [SerializeField]
    private GameObject cookSpawnEffect;

    [SerializeField]
    private Transform[] cookEffectPoints;

    [SerializeField]
    private GameObject[] _enemyRobotPrefabs;

    [SerializeField]
    private GameObject[] _enemyDrugPrefabs;
    private List<GameObject> finalEnemies = new List<GameObject>();


    [SerializeField]
    private float _spawnTimer = 2f;


 


    // enemy gang type : regular enemies, walking around the block and guarding the streets
    // health points: regular
    // special ability : performs attack combos 
    public void SpawnStreetEnemies(int enemyCount)
    {
        for(int i= 0; i< enemyCount; i++)
        {
           var Enemies = 
                Instantiate(_enemyRegularPrefabs[Random.Range(0, enemySpawnPoints.Length )],
                enemySpawnPoints[i].position, Quaternion.identity);
        }
    }


   // enemy gang type : more tactical robot enemies, gurading gate to buidling where enemy gang is creating drug called crystal
   // their purpose is to prevent anynone that is not from gang to enter the gate
   //health value is increased by 5 points
   // special ability: shoots from laser gun at longer range, perform special combos at closer range
   public void SpawnVipers(int enemy)
   {
        for (int i = 0; i < enemy; i++)
        {
            Instantiate(viperSpawnEffect, viperEffectSpawnPoints[i].position , Quaternion.Euler(-90f,0f,0f));
            Instantiate(_enemyRobotPrefabs[i], enemySpawnPoints[i].position, Quaternion.Euler(0f,50.0f,0f));
        }
   }

    // enemy gang type: beast gang members , high on crystal drug , they are cookers which means they are creating crystal drug for the gang 
    //health value is increased by 10 points
    // special ability: prefrom special combo at closer range , throws a crystal bomb at longer range 
    public void SpawnDrugCookers(int count)
    {
        for(int i = 0; i < count; i++)
        {
              Instantiate(_enemyDrugPrefabs[i], gateSpawnPoints[i].position, Quaternion.Euler(0f, 50f, 0f));
            
            Instantiate(cookSpawnEffect, cookEffectPoints[i].position, Quaternion.Euler(-90f, 0f,0f));
        }
    }




} // end
