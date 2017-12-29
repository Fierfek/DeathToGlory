using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    //Enumerated values for the type of item.
	public ItemEnums.ItemType type;
    public ItemEnums.ConsumeType consumableType;

    //Item Characteristics

    public int itemLevel;
    public int healthMod;
    public float movementMod;
    public int attackMod;
    public int magicAtkMod;
    public int magicDefMod;
    public int spiritMod;
    public int armorMod;

    //public int effectDuration;
    public string itemName;

    public bool hasRngHealth;
    public bool hasRngMove;
    public bool hasRngAtk;
    public bool hasRngMgkAtk;
    public bool hasRngMgkDef;
    public bool hasRngSpirit;
    public bool hasRngArmor;


    //Creates spaces for using sprites in the inventory slots.
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;


    private void Start()
    {
        if (hasRngHealth) { healthMod = Random.Range(0, 16) + (itemLevel * 10); }
        if (hasRngMove) { movementMod = Random.Range(0, 16) + (itemLevel * 10); }
        if (hasRngAtk) { attackMod = Random.Range(0, 16) + (itemLevel * 10); }
        if (hasRngMgkAtk) { magicAtkMod = Random.Range(0, 16) + (itemLevel * 10); }
        if (hasRngMgkDef) { magicDefMod = Random.Range(0, 16) + (itemLevel * 10); }
        if (hasRngSpirit) { spiritMod = Random.Range(0, 16) + (itemLevel * 10); }
        if (hasRngArmor) { armorMod = Random.Range(0, 16) + (itemLevel * 10); }
    }
	public void UseItem()
    {
       if(type == ItemEnums.ItemType.CONSUMABLE)
        {
            print("Item Consumed.");
        }
    }
}
