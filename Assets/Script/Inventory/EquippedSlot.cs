using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    // Slot appearance
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TMP_Text slotName;

    // Slot data
    [SerializeField]
    private ItemType itemType;

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    // Other variables
    private bool slotInUse;

    // Reference to the inventory manager
    private InventoryManagers inventoryManagers;

    void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas")?.GetComponent<InventoryManagers>();
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
            Debug.LogWarning("Slot is already in use. Unequip first.");
            return;
        }

        // Update slot appearance and data
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.text = itemName; // Set the slot name to the equipped item's name
        slotName.enabled = true; // Show the item name

        // Update other data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

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

        // Store item details for returning to the inventory
        Sprite returnedSprite = itemSprite;
        string returnedName = itemName;
        string returnedDescription = itemDescription;
        ItemType returnedItemType = itemType; // Store item type

        // Clear the slot
        this.itemSprite = null;
        slotImage.sprite = null;
        slotName.text = string.Empty; // Clear the name
        slotName.enabled = false; // Hide the name
        slotInUse = false;

        // Reset item type
        itemType = ItemType.none; // Or appropriate logic

        Debug.Log($"Gear unequipped: {returnedName}");

        // Return the item back to the appropriate equipment slot
        if (inventoryManagers != null)
        {
            inventoryManagers.ReturnToEquipmentSlot(returnedName, returnedSprite, returnedDescription, returnedItemType);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UnequipGear(); // Call unequip on right-click
        }
    }
}