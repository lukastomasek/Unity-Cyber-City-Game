using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 3;

    #region Singleton

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More then one invenotory was found");
            return;
        }
        else
        {
            instance = this;
        }
    }

    #endregion
  

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Inventory is full!");
                return false;
            }

            items.Add(item);

            if(onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }
        return true;
    }


    public void Remove(Item item)
    {
        items.Remove(item);

        if(onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }


}
