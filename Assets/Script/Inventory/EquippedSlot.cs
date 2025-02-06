using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquippedSlot : MonoBehaviour
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

        // Check if UI components are assigned
        if (slotImage == null)
        {
            Debug.LogError("Slot Image is not assigned. Cannot update appearance.");
            return;
        }
        if (slotName == null)
        {
            Debug.LogError("Slot Name is not assigned. Cannot update appearance.");
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
        // Clear the slot
        this.itemSprite = null;
        slotImage.sprite = null;
        slotName.text = string.Empty; // Clear the name
        slotName.enabled = false; // Hide the name
        slotInUse = false;

        Debug.Log("Gear unequipped.");
    }
}