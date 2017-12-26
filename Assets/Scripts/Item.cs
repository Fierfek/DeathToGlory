using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    //Enumerated values for the type of item.
	public ItemEnums.ItemType type;
    public ItemEnums.ConsumeType consumableType;

    //Item Characteristics
    public bool armor;

    public int healthMod;
    public float movementMod;
    public int attackMod;
    public int magicAtkMod;
    public int magicDefMod;
    public int spiritMod;
    public int armorRating;
    public int effectDuration;
    public string itemDescription;

    //Creates spaces for using sprites in the inventory slots.
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

	public void UseItem()
    {
       if(type == ItemEnums.ItemType.CONSUMABLE)
        {
            print("Item Consumed.");
        }
    }
}
