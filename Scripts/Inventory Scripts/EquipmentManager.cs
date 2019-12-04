using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    Equipment[] currentEquipment;

    private Inventory inventory;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChange;

    public GameObject gun, melee, keycaed;


    #region Singelton
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There are more instances of equipment manager !");
            return;
        }
        else
        {
            instance = this;
        }
    }

    #endregion


    private void Start()
    {
        inventory = Inventory.instance;

       int numSlots =  System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }


    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChange != null)
        {
            onEquipmentChange.Invoke( newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }


    public void UnEquip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);


            if(onEquipmentChange != null)
            {
                onEquipmentChange.Invoke(null, oldItem);
            }

            //uneqiping everything
            currentEquipment[slotIndex] = null;
        }
    }


    public void UnEquipAll()
    {
        for(int  i = 0; i < currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnEquipAll();
        }
    }



}// end 
