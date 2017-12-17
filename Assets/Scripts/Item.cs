using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    //Enumerated values for the type of item.
    public enum ItemType { HEAD, CHEST, ARMS, HANDS, LEGS, FEET, CONSUMABLE, ACCESSORY};
    public enum ConsumeType { HEALTH, GADGET};

    public ItemType type;
    public ConsumeType consumableType;

    //Creates spaces for using sprites in the inventory slots.
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

    //A script for using items. 
    //Not currently available. Needs more programming.
	public void UseItem()
    {
        switch (type)
        {
            case ItemType.CONSUMABLE:
                Debug.Log("The "+ consumableType+ " has been used.");
                break;
            default:

                break;
        }
    }
}
