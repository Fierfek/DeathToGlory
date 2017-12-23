using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UniqueSlot : Slot
{

	public ItemEnums.ItemType slotType;

    //Adds an item to the stack and increases the stack's item count to the slot.
    public void AddItem(Item newitem)
    {
        if (slotType == newitem.type)
        {
           ItemStack.Push(newitem);
           if (ItemStack.Count > 1)
            {
                stackText.text = ItemStack.Count.ToString();
            }

            ChangeSprite(newitem.spriteNeutral, newitem.spriteHighlighted);
        }
    }
}
