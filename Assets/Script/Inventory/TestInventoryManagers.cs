using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestInventoryManagers : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public TestItemSlot[] itemSlot;
    public ItemSO[] itemSOs;

    private GameObject draggedItem;
    private RectTransform draggedItemRectTransform;
    private CanvasGroup draggedItemCanvasGroup;
    private Vector2 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
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

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void BeginDrag(TestItemSlot itemSlot, PointerEventData eventData)
    {
        draggedItem = eventData.pointerDrag;
        draggedItemRectTransform = draggedItem.GetComponent<RectTransform>();
        draggedItemCanvasGroup = draggedItem.GetComponent<CanvasGroup>();

        originalPosition = draggedItemRectTransform.anchoredPosition;

        draggedItemCanvasGroup.alpha = 0.6f;
        draggedItemCanvasGroup.blocksRaycasts = false;
    }

    public void Drag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            draggedItemRectTransform.anchoredPosition += eventData.delta / CanvasScaleFactor();
        }
    }

    public void EndDrag(TestItemSlot itemSlot, PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            draggedItemCanvasGroup.alpha = 1f;
            draggedItemCanvasGroup.blocksRaycasts = true;

            if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<ItemSlot>() == null)
            {
                draggedItemRectTransform.anchoredPosition = originalPosition;
            }
            else
            {
                TestItemSlot targetSlot = eventData.pointerEnter.GetComponent<TestItemSlot>();
                if (targetSlot != null && targetSlot != itemSlot)
                {
                    SwapItems(itemSlot, targetSlot);
                }
            }

            draggedItem = null;
        }
    }

    private void SwapItems(TestItemSlot slotA, TestItemSlot slotB)
    {
        string tempName = slotA.itemName;
        int tempQuantity = slotA.quantity;
        Sprite tempSprite = slotA.itemSprite;
        string tempDescription = slotA.itemDescription;

        slotA.SetItem(slotB.itemName, slotB.quantity, slotB.itemSprite, slotB.itemDescription);
        slotB.SetItem(tempName, tempQuantity, tempSprite, tempDescription);
    }

    private float CanvasScaleFactor()
    {
        return GetComponentInParent<Canvas>().scaleFactor;
    }
}
