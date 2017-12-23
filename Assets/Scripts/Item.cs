using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    //Enumerated values for the type of item.


    public ItemEnums.ItemType type;
    public ItemEnums.ConsumeType consumableType;

    //Creates spaces for using sprites in the inventory slots.
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

    //A script for using items. 
    //Not currently available. Needs more programming.
	public void UseItem()
    {
       if(type == ItemEnums.ItemType.CONSUMABLE)
        {
            print("Item Consumed.");
        }
    }
}
