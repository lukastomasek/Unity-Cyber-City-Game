using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParent;
    public GameObject inventoryUI;


    private Inventory inventory;
    private InventorySlot[] slots;


    private void Start()
    {
        inventory = Inventory.instance;
        slots = itemParent.GetComponentsInChildren<InventorySlot>();
        
        inventory.onItemChangedCallBack += OnUpdateUI;
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }


    void OnUpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }

}
