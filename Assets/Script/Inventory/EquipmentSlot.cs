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
        inventoryManagers = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagers>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
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
        if (itemType == ItemType.head)
            headSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.body)
            bodySlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.leg)
            legSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.hand)
            handSlot.EquipGear(itemSprite, itemName, itemDescription);

        EmptySlot();
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

   
 

