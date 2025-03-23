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
    private EquippedSlot[] equippedSlots;

    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;



    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManagers inventoryManagers;

    public void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas")?.GetComponent<InventoryManagers>();

        equippedSlots = new EquippedSlot[4];
        equippedSlots[(int)ItemType.head] = GameObject.Find("HeadSlot")?.GetComponent<EquippedSlot>();
        equippedSlots[(int)ItemType.body] = GameObject.Find("BodySlot")?.GetComponent<EquippedSlot>();
        equippedSlots[(int)ItemType.leg] = GameObject.Find("LegSlot")?.GetComponent<EquippedSlot>();
        equippedSlots[(int)ItemType.hand] = GameObject.Find("HandSlot")?.GetComponent<EquippedSlot>();

        // Log if any equipped slot is not found
        //if (headSlot == null) Debug.LogError("HeadSlot not found!");
        //if (bodySlot == null) Debug.LogError("BodySlot not found!");
        //if (legSlot == null) Debug.LogError("LegSlot not found!");
        //if (handSlot == null) Debug.LogError("HandSlot not found!");
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if (isFull)
        {
            Debug.LogWarning("Slot is already full.");
            return 0; // or handle stacking logic if needed
        }

        // Update item data
        this.itemType = itemType;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;

        this.quantity = 1; // Set the quantity
        isFull = true; // Mark the slot as full

        // Update UI
        RefreshSlotUI();

        return 0; // Return any leftover items (if applicable)
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
            ItemData oldEquirp = GetOldEquipData();
            oldEquirp.DebugItem();

            EquipGear();
            EmptySlot();
           
        }
        else
        {
            inventoryManagers.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            RefreshDescUI();
        }
    }

    private ItemData GetOldEquipData()
    {
        EquippedSlot es = equippedSlots[(int)itemType];
        return es.GetEquipData();
    }

    private void EquipGear()
    {
        // Ensure itemType is set correctly
        Debug.Log($"Equipping: {itemName} of type {itemType}");

        equippedSlots[(int)itemType]?.EquipGear(itemSprite, itemName, itemDescription);

       
    }
    private void EmptySlot()
    {
        itemName = itemDescription = string.Empty;
        itemSprite = emptySprite;
        quantity = 0;
        isFull = false;
        RefreshSlotUI();
        RefreshDescUI();
    }

    public void OnRightClick()
    {

    }
    public void Deselect()
    {
        thisItemSelected = false;
        selectedShader.SetActive(false);
        RefreshDescUI(); // Clear the description UI if necessary
        Debug.Log("Slot deselected.");
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