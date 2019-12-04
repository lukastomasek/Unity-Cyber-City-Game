using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName ="Items/Weapons", order = 1)]
public class Item : ScriptableObject
{
    new public string name = "newItem";
    public Sprite icon = null;
    public float damage = 1;
    public bool isDefaultItem = false;
    public bool isGun;
    public bool isMelee;
    public bool isKeycard;
  
    public virtual void Use()
    {
        Debug.Log("Using Item" + name);


    }


    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
