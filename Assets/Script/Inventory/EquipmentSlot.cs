using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    // Item data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;

    // Item slot UI
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    // Equipped slots
    private EquippedSlot headSlot, bodySlot, legSlot, handSlot;

    // Description UI
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    // Selection
    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManagers inventoryManagers;

    public void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagers>();
        headSlot = GameObject.Find("HeadSlot").GetComponent<EquippedSlot>();
        bodySlot = GameObject.Find("BodySlot").GetComponent<EquippedSlot>();
        legSlot = GameObject.Find("LegSlot").GetComponent<EquippedSlot>();
        handSlot = GameObject.Find("HandSlot").GetComponent<EquippedSlot>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        Debug.Log($"Adding item: {itemName} to slot {this.name}");
        if (isFull)
        {
            Debug.LogWarning("Slot is already full.");
            return 0;
        }

        // Set item data
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.itemType = itemType;

        this.quantity = quantity;
        isFull = true;

        RefreshSlotUI();
        return quantity;
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
            EquipGear();
        }
        else
        {
            inventoryManagers.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            RefreshDescUI();
        }
    }

    private void EquipGear()
    {
        if (itemType == ItemType.head && headSlot != null)
            headSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.body && bodySlot != null)
            bodySlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.leg && legSlot != null)
            legSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.hand && handSlot != null)
            handSlot.EquipGear(itemSprite, itemName, itemDescription);

        EmptySlot();
    }

    public void OnRightClick()
    {
        if (thisItemSelected && quantity > 0)
        {
            UnequipGear();
        }
    }

    private void UnequipGear()
    {
        Debug.Log($"Unequipping item: {itemName} from slot {this.name}");
        inventoryManagers.AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
        EmptySlot();
    }

    private void EmptySlot()
    {
        itemName = string.Empty;
        itemDescription = string.Empty;
        itemSprite = emptySprite;
        quantity = 0;
        isFull = false;

        RefreshSlotUI();
        RefreshDescUI();
    }

    private void RefreshSlotUI()
    {
        quantityText.text = quantity > 0 ? quantity.ToString() : string.Empty;
        itemImage.sprite = itemSprite != null ? itemSprite : emptySprite;
    }

    private void RefreshDescUI()
    {
        if (string.IsNullOrEmpty(itemName))
        {
            ItemDescriptionNameText.text = string.Empty;
            ItemDescriptionText.text = string.Empty;
            itemDescriptionImage.sprite = emptySprite;
        }
        else
        {
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
        }
    }
}