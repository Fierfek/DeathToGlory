using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    //All the fields relating to the inventory height/width, inventory slot number, inventory row number,
    //and inventory layout.
    private RectTransform inventoryRectangle;
    private float inventoryWidth, inventoryHeight;
    public int slots;
    public int rows;
    public float slotPaddingLeft, slotPaddingTop;
    public float slotSize;
    public GameObject slotPrefab;
    private List<GameObject> allSlots;
    private static int emptySlots;

    public static int EmptySlots
    {
        get
        {
            return emptySlots;
        }

        set
        {
            emptySlots = value;
        }
    }



    //Initializes inventory.
    void Start () {
        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	// Creates an empty inventory with a number of slots and a number of rows specified by the editor.
    private void CreateLayout()
    {
        allSlots = new List<GameObject>();
        EmptySlots = slots;

        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHeight = (rows) * (slotSize + slotPaddingTop) + slotPaddingTop;
        inventoryRectangle = GetComponent<RectTransform>();
        inventoryRectangle.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRectangle.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

        int columns = slots / rows;
        for (int y = 0; y< rows; y++)
        {
            for(int x = 0; x<columns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                newSlot.name = "Slot";
                newSlot.transform.SetParent(this.transform.parent);
                slotRect.localPosition = inventoryRectangle.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y),0);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                allSlots.Add(newSlot);
            }
        }
    }


    //Adds an items to a slot.
    public bool AddItem(Item item)
    {
        if(item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }
        else
        {
            foreach(GameObject slot in allSlots)
            {
                Slot temporary = slot.GetComponent<Slot>();
                if (!temporary.IsEmpty)
                {
                    if(temporary.CurrentItem.type == item.type && temporary.IsAvailable)
                    {
                        temporary.AddItem(item);

                        return true;
                    }
                }
            }
            if(EmptySlots > 0)
            {
                PlaceEmpty(item);
            }
        }
        return false;
    }

    //Adds the item to an empty slot.
    private bool PlaceEmpty(Item addedItem)
    {
        if (EmptySlots > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot temp = slot.GetComponent<Slot>();
                if (temp.IsEmpty)
                {
                    temp.AddItem(addedItem);
                    EmptySlots--;
                    return true;
                }
            }
        }
        return false;
    }

}
