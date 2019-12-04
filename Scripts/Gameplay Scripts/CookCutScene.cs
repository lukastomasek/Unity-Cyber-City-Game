using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  THIS ALL SCRIPT COULD BE IN INTERFACE !!
/// </summary>
public class CookCutScene : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private Camera animCamera;

    [SerializeField]
    private float animTimer = 8f;

    private CharacterMovement playerMovement;
    private Animator anim;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterMovement>();
        anim = GetComponent<Animator>();
    }

    // this cut scene will play after player is at saving check point 1
    // this fucntion is called in game manager
    public void PlayCookCutScene2()
    {
        playerCamera.gameObject.SetActive(false);
        animCamera.gameObject.SetActive(true);
        playerMovement.StopPlayer();
       
        anim.Play("CutScene2Anim");
        anim.speed = 0.3f;
    }


    public void EnablePlayerMovementAndCamera()
    {
        StartCoroutine(EnablePlayer());
    }


    private IEnumerator EnablePlayer()
    {
        yield return new WaitForSecondsRealtime(animTimer);
        
        animCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        
     
    }

}
