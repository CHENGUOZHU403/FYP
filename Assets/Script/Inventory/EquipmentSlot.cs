using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    //itemdata
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;



    //itemslot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    //equipped slots
    private EquippedSlot headSlot, bodySlot, legSlot, handSlot;

    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;



    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManagers inventoryManagers;

    public void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas")?.GetComponent<InventoryManagers>();

        headSlot = GameObject.Find("HeadSlot")?.GetComponent<EquippedSlot>();
        bodySlot = GameObject.Find("BodySlot")?.GetComponent<EquippedSlot>();
        legSlot = GameObject.Find("LegSlot")?.GetComponent<EquippedSlot>();
        handSlot = GameObject.Find("HandSlot")?.GetComponent<EquippedSlot>();

        // Log if any equipped slot is not found
        if (headSlot == null) Debug.LogError("HeadSlot not found!");
        if (bodySlot == null) Debug.LogError("BodySlot not found!");
        if (legSlot == null) Debug.LogError("LegSlot not found!");
        if (handSlot == null) Debug.LogError("HandSlot not found!");
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if (isFull)
        {
            Debug.LogWarning("Slot is already full.");
            return 0; // or handle stacking logic if needed
        }
        //updata itemtype
        this.itemType = itemType;

        // TODO: Check if the item is the same?

        // Check if we still have space left in this slot

        // Set item data
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;

        this.quantity = 1;
        isFull = true;



        // Update UI
        RefreshSlotUI();

        return 0;
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
        Debug.Log($"Left clicked on slot: {itemName}");
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
        // Ensure itemType is set correctly
        Debug.Log($"Equipping: {itemName} of type {itemType}");
        if (itemType == ItemType.head && headSlot != null)
        {
            headSlot.EquipGear(itemSprite, itemName, itemDescription);
        }
        // Add similar checks for body, leg, hand
        else
        {
            Debug.LogError("Unable to equip gear: Invalid type or slot not found.");
        }
        EmptySlot(); // Clear the current slot after equipping
    }
    private void EmptySlot()
    {
        itemName = itemDescription = string.Empty;
        itemSprite = emptySprite;

        RefreshSlotUI();

    }

    public void OnRightClick()
    {

    }
    private void RefreshSlotUI()
    {
        //quantityText.text = quantity > 0 ? quantity.ToString() : string.Empty;
        itemImage.sprite = itemSprite != null ? itemSprite : emptySprite;
    }
    private void RefreshDescUI()
    {
        //ItemDescriptionNameText.text = itemName;
        //ItemDescriptionText.text = itemDescription;
        // itemDescriptionImage.sprite = itemSprite != emptySprite ? itemSprite : emptySprite;
    }
}