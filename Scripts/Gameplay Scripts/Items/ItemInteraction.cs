using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    public Item_SO itemSO;
    public Image healthBar;
    public GameObject itemEffect;

    private UniversalHealthController playerHealth;
    private float currentHealth;
    private float maxHealth = 100f;
    MissionManagerUI missionUI;


    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<UniversalHealthController>();

        if (playerHealth.isPlayer)
        {
            currentHealth = playerHealth.health;
        }

        missionUI = MissionManagerUI.instance;
    }


    private void ApplyMedkit()
    {
        if((playerHealth.health + itemSO.health) > maxHealth)
        {
            playerHealth.health = maxHealth;
        }
        else
        {
            playerHealth.health += itemSO.health;
        }

        healthBar.fillAmount = playerHealth.health / 100f;

        Destroy(gameObject, 0.2f);
        Destroy(itemEffect, 0.2f);
    }


    private void PickedUpKeyCard()
    {
        itemSO.itemPickedUp = true;
        Destroy(gameObject);
        Destroy(itemEffect,0.2f);
        missionUI.ShowUnlockGateTxt();
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.PLAYER))
        {

            if (itemSO.ishealth)
            {
                if (MaxHealth() == true)
                    return;

                ApplyMedkit();
            }
            else if (itemSO.isKeyCard)
            {
                PickedUpKeyCard();
                GameplayManager gm = FindObjectOfType<GameplayManager>();

                gm.SendMessage("HasKeyCard", itemSO.itemPickedUp);
            }
        }
    }


    private bool MaxHealth()
    {
        return playerHealth.health == maxHealth;
    }

  
}
