using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void UnlockGate()
    {
        anim.Play("GateOpenAnim");
        Debug.Log("Gate is opening!");
        gameObject.SetActive(false);
    }

     

}
