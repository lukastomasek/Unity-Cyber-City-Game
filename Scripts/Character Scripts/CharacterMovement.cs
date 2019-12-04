using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{

    public float speed;
    private float maxDistance = -896f;

    private Vector3 moveVelocity;
    private Vector3 moveInput;
    private Rigidbody rb;
    private Camera m_Camera;
    private CharacterAnimations animations;
    private Animator anim;
    private UniversalHealthController healthController;

    public Stats playerStats;
    private float currentHealth;
    //private FixedJoystick joystick;
    

    private void Awake()
    {
        //joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        anim = GetComponent<Animator>();
        healthController = GetComponent<UniversalHealthController>();
        
    }

    void Start()
    {
        m_Camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        animations = GetComponent<CharacterAnimations>();

       
    }

    private void FixedUpdate()
    {
        // JoysctickMovement();
        keyboardMovement();
    }


   private void Update()
    {
        rb.velocity = moveVelocity ;   
        anim.SetFloat("Speed", rb.velocity.magnitude);
              
    }

    #region Joystick Movement

    void JoysctickMovement()
    {
        //float horizontal = joystick.Horizontal;
        //float vertical = joystick.Vertical;

        //moveInput = new Vector3(horizontal, 0, vertical);
        //if (moveInput.sqrMagnitude > 1.1f)
        //{
        //    moveInput.Normalize();
        //}

        //Vector3 move_Forward = m_Camera.transform.forward;
        //move_Forward.y = 0;

        //Quaternion moveToRelativePosition = Quaternion.FromToRotation(Vector3.forward, move_Forward);
        //Vector3 look_Towards = moveToRelativePosition * moveInput;

        //if (moveInput.sqrMagnitude > 0)
        //{
        //    Ray lookRay = new Ray(transform.position, look_Towards);
        //    transform.LookAt(lookRay.GetPoint(1));
        //}

        //moveVelocity = transform.forward * speed * moveInput.sqrMagnitude;
    }

    #endregion

    void keyboardMovement()
    {
        float horizontal = Input.GetAxis(AxisTags.HORIZONTAL);
        float vertical = Input.GetAxis(AxisTags.VERTICAL);

        moveInput = new Vector3(horizontal, 0, vertical);
        if (moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }

        Vector3 move_Forward = m_Camera.transform.forward;
        move_Forward.y = 0;

        Quaternion moveToRelativePosition = Quaternion.FromToRotation(Vector3.forward, move_Forward);
        Vector3 look_Towards = moveToRelativePosition * moveInput;

        if (moveInput.sqrMagnitude > 0)
        {
            Ray lookRay = new Ray(transform.position, look_Towards);
            transform.LookAt(lookRay.GetPoint(1));
        }

        moveVelocity = transform.forward * speed * moveInput.sqrMagnitude;


        Vector3 temp = transform.position;

        if(temp.z >= maxDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxDistance);
        }

        transform.position = temp;

    }



  /// <summary>
  ///  these 2 functions stops player when cooks cutscene are player
  ///  there was an issue when the cut scene was plying player would keep running
  ///  stop player is preventing player to run when the cut scene is playing
  ///  enable player - enables player movement when cutscene is done
  /// </summary>

    public void StopPlayer()
    {
        enabled = false;
        Debug.Log("player movement is disabled");
    }

    public void EnablePlayer()
    {
        enabled = true;
        Debug.Log("player movement is enabled");
    }





} // end 
