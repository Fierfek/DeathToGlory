using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    //Enumerated values for the type of item.
	public ItemEnums.ItemType type;
    public ItemEnums.ConsumeType consumableType;

    //Item Characteristics

    public int healthMod;
    public float movementMod;
    public int attackMod;
    public int magicAtkMod;
    public int magicDefMod;
    public int spiritMod;
    public int armorMod;
    //public int effectDuration;
    public string itemName;

    //Creates spaces for using sprites in the inventory slots.
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;


    private void Start()
    {
        switch (type)
        {

        }
    }
	public void UseItem()
    {
       if(type == ItemEnums.ItemType.CONSUMABLE)
        {
            print("Item Consumed.");
        }
    }
}
