using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2.5f;

    private Transform InteractItemTransform;
    private Transform player;
    private bool hasInteract = false;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }


    public virtual void Interact()
    {
        Debug.Log("Player Interaced " + gameObject.name);
    }


    private void Update()
    {
        if (!hasInteract)
        {
            float distance = Vector3.Distance(player.position, InteractItemTransform.position);

            if(distance <= radius)
            {
                Interact();
                hasInteract = true;
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (InteractItemTransform == null)
            InteractItemTransform = transform;


        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractItemTransform.position, radius);
    }

}
