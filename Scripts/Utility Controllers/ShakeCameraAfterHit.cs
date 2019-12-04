using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShakeCameraAfterHit : MonoBehaviour
{
    
    public static ShakeCameraAfterHit enableShakeFX;

    private ShakeCamera shakeCamera;
    private CameraFollow cameraFollow;
    private CharacterAttackController characterAttack;

    public float shakeDuration = 0.5f;


    #region Singelton Pattern
    private void Awake() => Singelton();
  
    private void Singelton()
    {
        if (enableShakeFX == null)
            enableShakeFX = this;
    }

    #endregion


    void Start()
    {
        shakeCamera = FindObjectOfType<ShakeCamera>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        characterAttack =
            GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterAttackController>();
    }

 


    public void TurnOnShakeScript()
    {
        StartCoroutine(ShakeCamera());
    }


    IEnumerator ShakeCamera()
    {
       
            cameraFollow.enabled = false;
            shakeCamera.enabled = true;
            shakeCamera.InitializeValues(shakeDuration);

            yield return new WaitForSeconds(1f);
            cameraFollow.enabled = true;
            shakeCamera.enabled = false;
            characterAttack.ComboFinished = false;
        
    }

} // end 
