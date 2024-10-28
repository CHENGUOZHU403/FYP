using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerClickHandler
{
    //itemdata
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems;

    //itemslot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    //item description slot
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;




    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManagers inventoryManagers;

    public void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagers>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite,string itemDescription)
    {
        // TODO: Check if the item is the same?

        // Check if we still have space left in this slot
        int available = maxNumberOfItems - this.quantity;
        if (available <= 0)
            return quantity;

        // Take the avaiable amount of item
        int delta = quantity > available ? quantity - available : 0;

        // Set item data
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;

        // Update amount
        this.quantity += delta != 0 ? available : quantity;
        
        // Update UI
        RefreshSlotUI();

        return delta;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void OnLeftClick()
    {
        if (thisItemSelected && quantity > 0)
        { 
            inventoryManagers.UseItem(itemName);
            this.quantity -= 1;
            if (this.quantity <= 0)
                EmptySlot();
            else
                RefreshSlotUI();
        }
        else
        { 
            inventoryManagers.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            RefreshDescUI();
        }
    }

    private void EmptySlot()
    {
        itemName = itemDescription = string.Empty;
        itemSprite = emptySprite;

        RefreshSlotUI();
        RefreshDescUI();
    }

    public void OnRightClick()
    {

    }


    
    private void RefreshSlotUI()
    {
        quantityText.text = quantity > 0 ? quantity.ToString() : string.Empty;
        itemImage.sprite = itemSprite != null ? itemSprite : emptySprite;
    }

    private void RefreshDescUI()
    {
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite != emptySprite ? itemSprite : emptySprite;
    }
}
