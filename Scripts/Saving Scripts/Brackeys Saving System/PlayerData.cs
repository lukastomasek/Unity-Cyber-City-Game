using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float health;
    public int level;
    public bool checkpointCompletedAfterSaving;
    public bool hasplayedFirstTime;
    public float[]position;


    public PlayerData(UniversalHealthController player)
    {
        if (player.isPlayer)
        {
            health = player.health;

            // storring player position in float array
            // returning this values after saving in universal health controller because it has all values that
            // we need to store 

            position = new float[3];

            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;
        }


      
    }

}

[System.Serializable]
public class PlayerStats
{
    public int expRequired;
    public int level;

    public PlayerStats(int exp, int level)
    {
        this.expRequired = exp;
        this.level = level;
    }
    public PlayerStats()
    {

    }

    public void UnlockAttacks()
    {
        // more code 
    }
}
