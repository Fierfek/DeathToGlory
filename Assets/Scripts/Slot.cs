using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    //This has the stack with text for stack amount and sprites for the slot.
    private Stack<Item> itemStack;
    public Text stackText;
    public Sprite slotEmpty;
    public Sprite slotHighlight;

    //If the stack is empty, then the slot is empty.
    public bool IsEmpty
    {
        get { return itemStack.Count == 0; }
    }

	// Creates an empty stack for the empty slot.
	void Start () {
        itemStack = new Stack<Item>();
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform textRect = GetComponent<RectTransform>();

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
        itemStack.Push(newitem);
        if(itemStack.Count > 1)
        {
            stackText.text = itemStack.Count.ToString();
        }

        ChangeSprite(newitem.spriteNeutral, newitem.spriteHighlighted);
        Debug.Log("");
    }

    //Changes sprite depending on if the slot is selected or not.
    private void ChangeSprite(Sprite neutral, Sprite highlight)
    {
        GetComponent<Image>().sprite = neutral;

        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        GetComponent<Button>().spriteState = st;
    }
}
