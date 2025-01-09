using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagers : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;
    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;

    public ItemSO[] itemSOs;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("InventoryMenu"))
        { 
            Inventory(); 
        }

        if (Input.GetButtonDown("EqirpmentMenu"))
            { 
            Equipment(); 
            }
    }

    void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
        }

        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
        }

    }


    void Equipment()
    {
        if (EquipmentMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
        }

        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);
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
        else
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i] == null)
                {
                    Debug.LogError($"EquipmentSlot at index {i} is not initialized!");
                    continue;
                }

                if (!equipmentSlot[i].isFull && (equipmentSlot[i].itemName == itemName || equipmentSlot[i].quantity == 0))
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    Debug.Log($"Left over items after adding to equipment: {leftOverItems}");

                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    }

                    return leftOverItems;
                }
            }
            Debug.Log("No space in equipment slots.");
            return quantity;
        }
    }






    public void DeselectAllSlots()
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
    
}

public enum ItemType
{   use,
    mission,
    head,
    body,
    leg,
    hand,
    none
};