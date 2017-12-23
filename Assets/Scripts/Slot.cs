using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerClickHandler {

    //This has the stack with text for stack amount and sprites for the slot.
    private Stack<Item> itemStack;
    public Text stackText;
    public Sprite slotEmpty;
    public Sprite slotHighlight;
    

    //If the stack is empty, then the slot is empty.
    public bool IsEmpty
    {
        get { return ItemStack.Count == 0; }
    }

    public bool IsAvailable
    {
        get {return CurrentItem.maxSize > ItemStack.Count; }
    }

    public Item CurrentItem
    {
        get { return ItemStack.Peek(); }
    }

    public Stack<Item> ItemStack
    {
        get
        {
            return itemStack;
        }

        set
        {
            itemStack = value;
        }
    }

    // Creates an empty stack for the empty slot.
    void Awake () {
        ItemStack = new Stack<Item>();
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform textRect = stackText.GetComponent<RectTransform>();

        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
        stackText.resizeTextMaxSize = textScaleFactor;
        stackText.resizeTextMinSize = textScaleFactor;

        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Adds an item to the stack and increases the stack's item count to the slot.
    public void AddItem(Item newitem)
    {
        ItemStack.Push(newitem);
        if(ItemStack.Count > 1)
        {
            stackText.text = ItemStack.Count.ToString();
        }

        ChangeSprite(newitem.spriteNeutral, newitem.spriteHighlighted);
    }

    public void AddItems(Stack<Item> itemStack)
    {
        /*this.ItemStack = new Stack<Item>(itemStack);
        stackText.text = itemStack.Count > 1 ? itemStack.Count.ToString() : string.Empty;
        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);*/

		foreach(Item i in itemStack) {
			AddItem(i);
		}

    }

    //Changes sprite depending on if the slot is selected or not.
    protected void ChangeSprite(Sprite neutral, Sprite highlight)
    {
        GetComponent<Image>().sprite = neutral;

        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        GetComponent<Button>().spriteState = st;
    }

    private void UseItem()
    {
        if (!IsEmpty)
        {
            ItemStack.Pop().UseItem();
            stackText.text = ItemStack.Count > 1 ? ItemStack.Count.ToString() : string.Empty;

            if (IsEmpty)
            {
                ChangeSprite(slotEmpty, slotHighlight);
                Inventory.EmptySlots++;
            }
        }
    }

    public void ClearSlot()
    {
        itemStack.Clear();
        ChangeSprite(slotEmpty, slotHighlight);
        stackText.text = string.Empty; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }
}
