using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Item data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems;

    // Item slot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    // Item description slot
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;
    public GameObject selectedShader;
    public bool thisItemSelected;
    private TestInventoryManagers inventoryManagers;

    public void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas").GetComponent<TestInventoryManagers>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // Check if we still have space left in this slot
        int available = maxNumberOfItems - this.quantity;
        if (available <= 0)
            return quantity;

        // Take the available amount of item
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

    public void SetItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;

        RefreshSlotUI();
        RefreshDescUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventoryManagers.BeginDrag(this, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryManagers.Drag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryManagers.EndDrag(this, eventData);
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
