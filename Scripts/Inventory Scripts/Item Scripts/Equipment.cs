using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EquipmentSlot
{
    WEAPON, MELEE, KEYCARD
}


[CreateAssetMenu(fileName ="NewEquipment", menuName ="Inventory/Equipment", order =2)]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public GameObject gun;


    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
       

    }


   

}
