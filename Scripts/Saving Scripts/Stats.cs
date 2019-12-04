using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{
    [Header("player health after saving")]
    public float playerHealth;
   
}

[System.Serializable]
public struct PlayerPosition
{
    public float posX;
    public float poY;
    public float posZ;


    public Vector3 GetPlayerPosition()
    {
        Vector3 pos = new Vector3(posX, poY, posZ);

        return pos;
    }
}
