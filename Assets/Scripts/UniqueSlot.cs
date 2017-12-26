using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UniqueSlot : Slot
{

	public ItemEnums.ItemType slotType;

	void Start() {
		tag = "Unique";
	}

	//Adds an item to the stack and increases the stack's item count to the slot.
	public new void AddItem(Item newitem) {

		print(newitem.type + " to " + slotType.ToString());

		if (slotType == newitem.type) {
			ItemStack.Push(newitem);
			if (ItemStack.Count > 1) {
				stackText.text = ItemStack.Count.ToString();
			}

			ChangeSprite(newitem.spriteNeutral, newitem.spriteHighlighted);
		}
	}

	public new void AddItems(Stack<Item> itemStack) {
        /*this.ItemStack = new Stack<Item>(itemStack);
        stackText.text = itemStack.Count > 1 ? itemStack.Count.ToString() : string.Empty;
        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);*/
        //There is an error where the stack goes higher than the max item stack size.
        foreach (Item i in itemStack) {
			AddItem(i);
		}

	}
}
