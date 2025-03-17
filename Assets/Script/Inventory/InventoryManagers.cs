using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagers : MonoBehaviour
{
    public GameObject playerInfoUI;
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;
    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;

    public ItemSO[] itemSOs;

    void Start()
    {
        InventoryMenu.SetActive(false);

        // Initialize equipment slots if they are not set
        if (equipmentSlot == null || equipmentSlot.Length == 0)
        {
            equipmentSlot = GetComponentsInChildren<EquipmentSlot>();
            Debug.Log("Equipment slots initialized dynamically.");
        }

        // Log the count of equipment slots
        Debug.Log($"Number of equipment slots: {equipmentSlot.Length}");
    }

    void Update()
    {
        if (Input.GetButtonDown("InventoryMenu"))
        {
            Inventory();
        }

        if (Input.GetButtonDown("EquipmentMenu"))
        {
            Equipment();
        }
    }

    public void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            playerInfoUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
            playerInfoUI.SetActive(false);
        }
    }

    public void Equipment()
    {
        if (EquipmentMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            playerInfoUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);
            playerInfoUI.SetActive(false);
        }
    }

    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
            }
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        Debug.Log($"Attempting to add item: {itemName}, Quantity: {quantity}, Type: {itemType}");

        if (itemType == ItemType.mission || itemType == ItemType.use)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i] == null)
                {
                    Debug.LogError($"ItemSlot at index {i} is not initialized!");
                    continue;
                }

                if (!itemSlot[i].isFull && (itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0))
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);

                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    }

                    return leftOverItems;
                }
            }
            Debug.Log("No space in item slots.");
            return quantity;
        }
        else // For equipment items
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (!equipmentSlot[i].isFull && equipmentSlot[i].itemName == string.Empty)
                {
                    // Attempt to add the item to the equipment slot.
                    equipmentSlot[i].AddItem(itemName, 1, itemSprite, itemDescription, itemType);
                    return 0; // Successfully added back to equipment slot
                }
            }
            Debug.Log("No space in equipment slots to return the item.");
            return quantity; // Return the leftover quantity if no slots are available
        }
    }
    public void ReturnToEquipmentSlot(string itemName, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            if (equipmentSlot[i].itemType == itemType && !equipmentSlot[i].isFull)
            {
                equipmentSlot[i].AddItem(itemName, 1, itemSprite, itemDescription, itemType);
                Debug.Log($"Returned {itemName} to {equipmentSlot[i].name}.");
                return;
            }
        }

        // If no equipment slot is available, add the item to the item slots
        Debug.Log("No available equipment slots to return the item. Adding to item slots.");
        AddItem(itemName, 1, itemSprite, itemDescription, itemType);
    }
    public void DeselectAllSlots()
    {
        // Deselect all item slots
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i] != null)
            {
                itemSlot[i].selectedShader.SetActive(false);
                itemSlot[i].thisItemSelected = false;
            }
        }

        // Deselect all equipment slots
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            if (equipmentSlot[i] != null)
            {
                equipmentSlot[i].Deselect(); // Call the Deselect method on each EquipmentSlot
            }
        }
    }
}

public enum ItemType
{
    head,
    body,
    hand,
    leg,
    use,
    mission,
    none
};