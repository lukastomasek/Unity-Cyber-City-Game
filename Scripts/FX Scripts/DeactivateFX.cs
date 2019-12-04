using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFX : MonoBehaviour
{
    [Range(0.5f, 2.2f)]
    [SerializeField]
    private float _deactivateTimer = 1.2f;
   
    void Start()
    {
        Destroy(gameObject, _deactivateTimer);
    }

    

}
