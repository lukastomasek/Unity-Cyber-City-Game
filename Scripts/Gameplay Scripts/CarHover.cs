using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHover : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.1f;
    private float positionY;

    private void Awake()
    {
        positionY = transform.position.y;
    }


    private void Update()
    {
        Vector3 hoverPosition = transform.position;
        hoverPosition =
            new Vector3(hoverPosition.x, positionY + Mathf.Sin(Time.time * speed) * 0.5f,hoverPosition.z );
        transform.position = hoverPosition;
    }
}
