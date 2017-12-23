using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private static Slot fromSlot, toSlot;
    private List<GameObject> allSlots;

    public GameObject iconPrefab;

    private static GameObject hoverObject;

    private static int emptySlots;

    public Canvas canvas;

    private float hoverYOffset;

    public EventSystem eventSystem;

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


        if (Input.GetMouseButtonUp(0))
        {
            if (!eventSystem.IsPointerOverGameObject(-1) && fromSlot != null)
            {
                fromSlot.GetComponent<Image>().color = Color.white;
                fromSlot.ClearSlot();
                Destroy(GameObject.Find("Hover"));
                toSlot = null;
                fromSlot = null;
                hoverObject = null;
            }
        }
		if(hoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffset);
            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }
	}


	// Creates an empty inventory with a number of slots and a number of rows specified by the editor.
    private void CreateLayout()
    {
        allSlots = new List<GameObject>();
        hoverYOffset = slotSize * 0.01f;
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

    public void MoveItem(GameObject clicked)
    {
        if(fromSlot == null)
        {
            if (!clicked.GetComponent<Slot>().IsEmpty)
            {
                fromSlot = clicked.GetComponent<Slot>();
                fromSlot.GetComponent<Image>().color = Color.gray;

                hoverObject = (GameObject) Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
                hoverObject.name = "Hover";

                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
                hoverObject.transform.localScale = fromSlot.gameObject.transform.localScale;
            }
        }
        else if(toSlot == null)
        {
            toSlot = clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));
        }
        if(toSlot != null && fromSlot != null)
        {
            Stack<Item> tempToSlot = new Stack<Item>(toSlot.ItemStack);
            toSlot.AddItems(fromSlot.ItemStack);

            if(tempToSlot.Count == 0)
            {
                fromSlot.ClearSlot();
            }
            else
            {
                fromSlot.AddItems(tempToSlot);
            }

            fromSlot.GetComponent<Image>().color = Color.white;
            toSlot = null;
            fromSlot = null;
        }
    }

}
