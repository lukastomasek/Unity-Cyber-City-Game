using UnityEngine;

public class ItemPickedUp : Interactable
{

    public Item item;
    

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }


    private void PickUp()
    {
        Debug.Log("Picked Up" + item.name);

        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
            
        }


      
    }

}
