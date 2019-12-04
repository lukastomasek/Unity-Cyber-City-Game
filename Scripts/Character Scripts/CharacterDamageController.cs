using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageController : MonoBehaviour
{
    public float damage = 5f;
    public LayerMask layerMask;
    public int  radius = 1;
    public bool is_Player;
    public bool is_Enemy;

    public GameObject bloodFX;
    public Transform bloodPoint;
    public GameObject punchEffect;

    void Update()
    {
        DealDamage();
    }


    void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if(hits.Length > 0)
        {
            if (is_Player)
            {
                // Debug.Log("ENEMY DAMAGED [5]");
                Vector3 hitPosition = hits[0].transform.position;
                hitPosition.y += 1.5f;

                if (hits[0].transform.forward.x > 0f)
                {
                    hitPosition.x += 0.4f;
                }
                else if (hits[0].transform.forward.x < 0f)
                {
                    hitPosition.x -= 0.4f;
                }

                var punches = Instantiate(punchEffect, hitPosition, Quaternion.identity);
                var blood =  Instantiate(bloodFX, bloodPoint.position, bloodPoint.rotation);

                hits[0].GetComponent<UniversalHealthController>().ApplyDamage(damage);

            }
            if (is_Enemy)
            {
              //  Debug.Log("PLAYER DAMAGED [5]");
                hits[0].GetComponent<UniversalHealthController>().ApplyDamage(damage);
                Instantiate(bloodFX, bloodPoint.position, bloodPoint.rotation);
              
            }
           
        }
        gameObject.SetActive(false);
    }




} // end
