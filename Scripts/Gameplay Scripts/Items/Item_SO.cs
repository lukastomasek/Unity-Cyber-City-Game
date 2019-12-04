using UnityEngine;


[CreateAssetMenu(fileName = "MedKit", menuName = "Item/Health", order = 3)]
public class Item_SO : ScriptableObject
{
    public string itemName = "health";

    [Range(1.1f, 30.5f)]
    public float health = 25f;

    [TextArea]
    public string itemPurpose;

    [HideInInspector]
    public bool itemPickedUp;

    public bool ishealth;
    public bool isKeyCard;

}
