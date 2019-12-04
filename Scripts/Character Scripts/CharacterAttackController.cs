using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ComboPunch
{
    NONE,PUNCH_1, PUNCH_2, PUNCH_3
}



[RequireComponent(typeof(CharacterAnimations))]
public class CharacterAttackController : MonoBehaviour
{
    private CharacterAnimations attackAnim;
    private UniversalHealthController healthController;
    private PlayerBlocking blocking;
    private CharacterMovement movement;

    private Button attackButton;
    private Button kickButton;
    private Button specialAttack_Button;
    private ComboPunch comboPunch;
   // private ColorBlock buttonColor;
    [SerializeField] private float defaultAttack_Timer = 0.05f;
    // give little bit time to perform attack before reseting the combo 
    private float resetPitch = 1f;
    private float currentTimer;
    private bool attackTimer_Reset;
    private bool comboFinished;
    private Animator animator;


    [SerializeField]
    private float specialAttackTimer = 8.5f;
    [SerializeField]
    private float currentSpecialTimer = 8.5f;
    [SerializeField]
    private Image specialAttackImg;

    private string specialAttackMessage = "special attack activated";
    private bool canPerformSpecialAttack;
    private bool buttonPressed = false;

    // informing game manager to set time scale to 0.6
    [HideInInspector]
    public bool specialAttackActivated;
    private SpecialEffectController effectController;

    void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        blocking = GetComponent<PlayerBlocking>();
        attackAnim = GetComponent<CharacterAnimations>();
        healthController = FindObjectOfType<UniversalHealthController>();
        animator = GetComponent<Animator>();
        effectController = SpecialEffectController.instance;
        #region Mobile Buttons
        // attackButton = GameObject.Find("Attack Button").GetComponent<Button>();
        // kickButton = GameObject.Find("Kick Button ").GetComponent<Button>();
        // specialAttack_Button = GameObject.Find("Special Attack Button").GetComponent<Button>();
        //// buttonColor = GameObject.Find("Special Attack Button").GetComponent<Button>().colors;
        #endregion

    }




    void Start()
    {
        comboPunch = ComboPunch.NONE;
        currentTimer = defaultAttack_Timer;

        #region SPECIAL ATTACK COLOUR BUTTON
        // button color in the start of the game 
        /*   buttonColor.normalColor = Color.gray;
           buttonColor.selectedColor = Color.gray;
           buttonColor.highlightedColor = Color.gray;
           buttonColor.pressedColor = Color.gray;
           buttonColor.disabledColor = Color.gray;
           specialAttack_Button.colors = buttonColor;*/

        #endregion

        #region Mobile Buttons
        //attackButton.onClick.AddListener(() => Attack());
        //kickButton.onClick.AddListener(() => Kick());
        //specialAttack_Button.onClick.AddListener(() => SpecialAttack());
        #endregion

    }

    void Update()
    {     

        Attack();
        Kick();
        ResetAttack();
      //  SpecialAttack();
        Block();
       // CheckForSpecialAttack();
    }

    void Attack()
    {
      
        if (Input.GetKeyDown(KeyCode.K))
        {
            comboPunch++;
            attackTimer_Reset = true;
            currentTimer = defaultAttack_Timer;

            if (comboPunch != ComboPunch.PUNCH_3)
            {
                if (comboPunch == ComboPunch.PUNCH_1)
                {
                    attackAnim.Punch1();
                }
                if (comboPunch == ComboPunch.PUNCH_2)
                {
                    attackAnim.Punch2();
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (comboPunch == ComboPunch.PUNCH_1)
                return;

            if(comboPunch == ComboPunch.PUNCH_2)
            {
                comboPunch++;
                attackTimer_Reset = true;
                currentTimer = defaultAttack_Timer;
            }

            if (comboPunch == ComboPunch.PUNCH_3)
            {
                attackAnim.Punch3();
                comboFinished = true;
            }
        }
     
        
    }

    void ResetAttack()
    {
        if (attackTimer_Reset)
        {
          
            currentTimer -= Time.deltaTime * resetPitch;
            if(currentTimer <= 0)
            {
                comboPunch = ComboPunch.NONE;
               
                currentTimer = defaultAttack_Timer;
                attackTimer_Reset = false;
            }
        }

    } // reset attack


    void Kick()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            attackAnim.QuakeKick();
        }
    }



    void SpecialAttack()
    {
        if (!canPerformSpecialAttack)
            return;
      
        if (Input.GetKeyDown(KeyCode.X))
        {
           
            if (canPerformSpecialAttack)
            {
                effectController.PlaySpecialAttack();
               Debug.Log($"Attack Activated : {specialAttackMessage}");
               attackAnim.SpecialAttack();

                // preventing game manager to set time scale  to 1
                // after calling special attack
             
               animator.speed = 1.2f;
               movement.speed = 4f;
            } 
              

        }
              
    }


    void CheckForSpecialAttack()
    {
        if(specialAttackTimer == currentSpecialTimer && Input.GetKeyDown(KeyCode.X ))
        {
            canPerformSpecialAttack = true;
            specialAttackActivated = true;
            ResetSpecialAttack();
        } 
        else if(specialAttackTimer <= 0f)
        {
            canPerformSpecialAttack = false;
            specialAttackActivated = false;
        }
    }

    

    void ResetSpecialAttack()
    {
        StartCoroutine(DecrementTimer());
    }

   

    IEnumerator DecrementTimer()
    {
        // this loop will reset the timer for  special attack and set back everything to normal
        while(true)
        {
            yield return new WaitForSeconds(3f);
            specialAttackTimer --;
           // specialAttackImg.fillAmount = specialAttackTimer / 100f;

            if(specialAttackTimer <= 0f)
            {
                specialAttackTimer = 0f;
                animator.speed = 1.0f;
                movement.speed = 3.5f;
                effectController.DeactivateParticleSystem();
                break;
            }
        }
       
    }
  

    void Block()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            attackAnim.BlockingAnim(true);
            blocking.IsBlocking(true);
            movement.enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            attackAnim.BlockingAnim(false);
            blocking.IsBlocking(false);
            attackAnim.UnFreezeAnim();
            movement.enabled = true;
        }
    }

    public bool ComboFinished
    {
        get { return comboFinished;  }
        set { comboFinished = value; }
    }


 


} // end
