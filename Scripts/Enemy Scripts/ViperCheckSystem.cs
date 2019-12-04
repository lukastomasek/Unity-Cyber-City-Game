using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViperCheckSystem : MonoBehaviour
{
    public static ViperCheckSystem instance;

  
    [HideInInspector]
    public bool enableKeycard;

    public int vipersCount = 3;

    #region Signelton
    private void Awake() => Singelton();

    private void Singelton()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    private void LateUpdate()
    {
        ViperDeadCheck();
    }



    private void ViperDeadCheck()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY);
        foreach(var p in enemies)
        {
            var viperHealth = p.GetComponent<UniversalHealthController>();

            if(viperHealth != null)
            {
                if (viperHealth.isViper)
                {
                    if (viperHealth.health <= 0f)
                    {
                        vipersCount--;
                    }
                }
            }
        }


        if (vipersCount == 0)
        {
            vipersCount = 0;
            enableKeycard = true;
        }
    }


    

}
