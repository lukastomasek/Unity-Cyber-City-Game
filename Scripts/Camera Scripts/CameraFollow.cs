using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Transform myPos;

    private float offset_Z = -895f;
    public float _contstant_Y = 1.65f;
    public float offset_X = 2.45f;
    public float lerp_Time = 1f;

    

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myPos = transform;
    }


    
    void LateUpdate()
    {
        if (target)
        {

            Vector3 follow = new Vector3(target.position.x + offset_X, target.position.y + _contstant_Y, offset_Z);
            transform.position = Vector3.Lerp(transform.position, follow, lerp_Time);
           
        }
        
    }

    

} // end
