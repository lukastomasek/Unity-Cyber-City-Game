using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 1.0f;
    private float _angle;

    public bool rotateHorizontally;
   

    // Update is called once per frame
    void Update()
    {
        if (rotateHorizontally)
        {
            RotateHorizontally();
        }
        else
        {
            RotateVertically();
        }
        
    }

    private void RotateVertically()
    {
        _angle = (_angle + _rotateSpeed) % 360f;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, _angle, 0f));
    }


    private void RotateHorizontally()
    {
        transform.Rotate(0f, 0f, 25 * Time.deltaTime);
    }

}
