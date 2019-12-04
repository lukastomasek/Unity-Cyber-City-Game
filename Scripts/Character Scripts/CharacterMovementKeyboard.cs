using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementKeyboard : MonoBehaviour
{
    private BaseMovementKeyboard baseMovement;
    private CharacterAnimations anim;

    private Vector3 screenMovementForward, screenMovementRight;
    private Quaternion screenMovement;

    [SerializeField]
    private Camera mainCamera;

    
    void Awake()
    {
        baseMovement = GetComponent<BaseMovementKeyboard>();
        anim = GetComponent<CharacterAnimations>();
        baseMovement.MoveDirection =  Vector3.zero;
        screenMovement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        screenMovementForward = screenMovement * Vector3.forward;
        screenMovementRight = screenMovement * Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        baseMovement.MoveDirection = Input.GetAxis(AxisTags.HORIZONTAL) * screenMovementRight +
            Input.GetAxis(AxisTags.VERTICAL) * screenMovementForward;

        if(baseMovement.MoveDirection.sqrMagnitude > 1)
        {
            baseMovement.MoveDirection.Normalize();
        }

        if(Input.GetAxis(AxisTags.HORIZONTAL) != 0f || Input.GetAxis(AxisTags.VERTICAL) != 0f)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);
        }
    }

}// end
