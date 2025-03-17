using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    // Slot appearance
    [SerializeField]
    private Image slotImage;
    [SerializeField]
    private TMP_Text slotName;

    // Add a reference to the UI mask sprite
    [SerializeField]
    private Sprite uiMaskSprite;

    // Slot data
    [SerializeField]
    private ItemType itemType;
    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    // Other variables
    private bool slotInUse;
    private InventoryManagers inventoryManagers;
    private EqiupmentSOLibrary eqiupmentSOLibrary;

    void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas")?.GetComponent<InventoryManagers>();
        slotImage = GetComponent<Image>();

        eqiupmentSOLibrary= GameObject.Find("InventoryCanvas")?.GetComponent<EqiupmentSOLibrary>();
        //slotName = GetComponentInChildren<TMP_Text>();
    }

    public ItemData GetEquipData()
    {
        ItemData itemData = new ItemData();
        itemData.itemName = itemName;
        itemData.sprite = itemSprite;
        itemData.itemType = itemType;

        Debug.Log("itemName :"+ itemName);
        return itemData;
    }

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        // Check for null item data
        if (itemSprite == null || string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(itemDescription))
        {
            Debug.LogError("Invalid item data: Cannot equip gear.");
            return; // Exit if item data is invalid
        }

        // Check if the slot is already in use
        if (slotInUse)
        {

            UnequipGear();
        }

        // Update slot appearance and data
       this.itemSprite = itemSprite;
       slotImage.sprite = this.itemSprite;
       slotName.text = itemName; // Set the slot name to the equipped item's name
       slotName.enabled = true; // Show the item name

        // Update other data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        //Update stats
        for (int i = 0; i < eqiupmentSOLibrary.equipmentSO.Length; i++)
        {
            if (eqiupmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                eqiupmentSOLibrary.equipmentSO[i].EquipItem();
        }


        slotInUse = true;
        Debug.Log($"Equipped: {itemName}");
    }

    public void UnequipGear()
    {
        if (!slotInUse)
        {
            Debug.LogWarning("No gear to unequip.");
            return;
        }

        //Store item details for returning to the inventory
        Sprite returnedSprite = itemSprite;
        string returnedName = itemName;
       string returnedDescription = itemDescription;
        ItemType returnedItemType = itemType; // Store item type

        // Clear the slot
        this.itemSprite = null;
        slotImage.sprite = uiMaskSprite; // Set the slot sprite to the UI mask sprite
        slotName.text = string.Empty; // Clear the name
        slotName.enabled = false; // Hide the name
        slotInUse = false;

         //Return the item back to the appropriate equipment slot
      if (inventoryManagers != null)
        {
            inventoryManagers.ReturnToEquipmentSlot(returnedName, returnedSprite, returnedDescription, returnedItemType);
       }

        //Update stats
        for (int i = 0; i < eqiupmentSOLibrary.equipmentSO.Length; i++)
        {
            if (eqiupmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                eqiupmentSOLibrary.equipmentSO[i].UnEquipItem();
        }
        EmptySlot();
    }

    //private void RefreshSlotUI()
    //{
       // slotImage.sprite = itemSprite != null ? itemSprite : uiMaskSprite;
  //  }

    private void EmptySlot()
    {
        itemName = string.Empty;
        itemDescription = string.Empty;
        itemSprite = uiMaskSprite;

        slotInUse = false;

        slotImage.sprite = itemSprite != null ? itemSprite : uiMaskSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UnequipGear(); // Call unequip on right-click
        }
    }
}