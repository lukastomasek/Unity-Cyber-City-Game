using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViperCutScene : MonoBehaviour
{
    [SerializeField]
    private Camera animCamera;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private float animTimer = 2f;

    private Animator anim;
    private CharacterMovement movement;

    public delegate void MovementDelegate();
    public static event MovementDelegate EnablePlayerMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = FindObjectOfType<CharacterMovement>();
    }

    public void EnableCutScene1()
    {
        playerCamera.gameObject.SetActive(false);
        animCamera.gameObject.SetActive(true);
        //Time.timeScale = 0;
        movement.enabled = false;
        anim.Play("SpawnViperAnim");

    }

    public void EnablePlayerMovementAndCamera()
    {
        StartCoroutine(enablePlayer());

       // EnablePlayerMovement.Invoke();
    }



    private IEnumerator enablePlayer()
    {
 
        yield return new WaitForSecondsRealtime(animTimer);
        animCamera.gameObject.SetActive(false);
        movement.enabled = true;
        Time.timeScale = 1;
        playerCamera.gameObject.SetActive(true);

    }
}
