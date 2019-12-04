using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovementKeyboard : MonoBehaviour
{
    private Rigidbody myBody;
    private Vector3 movementDirection;

    public float walkSpeed = 5f;
    public float walkingForce = 10f;
    public float turnSpeed = .4f;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = movementDirection * walkSpeed;
        Vector3 deltaVelocity = targetVelocity - myBody.velocity;

        if (myBody.useGravity)
        {
            deltaVelocity.y = 0;
        }

        myBody.AddForce(deltaVelocity * walkingForce, ForceMode.Acceleration);
        Vector3 faceDirection = movementDirection;
        
        if(faceDirection == Vector3.zero)
        {
            myBody.angularVelocity = Vector3.zero;
        }
        else
        {
            float rotationAngle = AngleAroundAxis(transform.forward, faceDirection, Vector3.up);
            myBody.angularVelocity = (Vector3.up * rotationAngle * turnSpeed);
        }

    }

    private float AngleAroundAxis(Vector3 axis, Vector3 dirA, Vector3 dirB)
    {
        float angle = Vector3.Angle(dirA, dirB);
        return angle * (Vector3.Dot(axis, Vector3.Cross(dirA,dirB))> 0? 1:-1); 
    }

    public Vector3 MoveDirection
    {
        get { return movementDirection; }
        set { movementDirection = value; }
    }
}
