using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlocking : MonoBehaviour
{
    private UniversalHealthController healthController;


    private void Awake()
    {
        healthController = GetComponent<UniversalHealthController>();
    }

    public void IsBlocking(bool block)
    {
        healthController.Blocking = block;
    }


}
