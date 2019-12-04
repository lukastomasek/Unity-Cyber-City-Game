using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocation : MonoBehaviour
{
    private float thisCharacterPosition_X;
    private float thisCharacterPosition_Y;
    private float thisCharacterPosition_Z;

    private float thisCharacterRotation_X;
    private float thisCharacterRotation_Y;
    private float thisCharacterRotation_Z;


    private float initialPlayerPosX = -109.75f;
    private float initialPlayerPosY = 648.8f;
    private float initialPlayerPosZ = -900f;


    private UniversalHealthController healthController;

    // getting values for position and rotation
    private void Awake()
    {
        thisCharacterPosition_X = PlayerPrefs.GetFloat("myPositionX");
        thisCharacterPosition_Y = PlayerPrefs.GetFloat("mypositionY");
        thisCharacterPosition_Z = PlayerPrefs.GetFloat("myPositionZ");

        thisCharacterRotation_X = PlayerPrefs.GetFloat("myRotationX");
        thisCharacterRotation_Y = PlayerPrefs.GetFloat("myRotationY");
        thisCharacterRotation_Z = PlayerPrefs.GetFloat("myRotationZ");

      

        healthController = GetComponent<UniversalHealthController>();
    }

    //setting the values for the current position and rotation
    private void Start()
    {
        transform.position = new Vector3(thisCharacterPosition_X, thisCharacterPosition_Y, thisCharacterPosition_Z);
        transform.rotation = Quaternion.Euler(thisCharacterRotation_X, thisCharacterRotation_Y, thisCharacterRotation_Z);

    }
    //saving players current position and rotation every frame
    private void LateUpdate()
    {
        SavePlayerPositionAndRotation();
    }


    public void SavePlayerPositionAndRotation()
    {
        PlayerPrefs.SetFloat("myPositionX", transform.position.x);
        PlayerPrefs.SetFloat("mypositionY", transform.position.y);
        PlayerPrefs.SetFloat("myPositionZ", transform.position.z);

        PlayerPrefs.SetFloat("myRotationX", transform.eulerAngles.x);
        PlayerPrefs.SetFloat("myRotationY", transform.eulerAngles.y);
        PlayerPrefs.SetFloat("myRotationZ", transform.eulerAngles.z);

      
    }


    public void InitialPlayerPosition()
    {
        transform.position = new Vector3(initialPlayerPosX, initialPlayerPosY, initialPlayerPosZ);
    }


    //private void OnGUI()
    //{
    //    if(GUI.Button(new Rect(Screen.width/2 ,50f,100,30),"Initial Position"))
    //    {
    //        InitialPlayerPosition();
    //    }
    //}
}
