using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerClickHandler
{
    //itemdata
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems;

    //itemslot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    //item description slot
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

    public int AddItem(string itemName, int quantity, Sprite itemSprite,string itemDescription)
    {
        //check slot is full
        if (isFull)
        {
            return quantity;
        }
        this.itemName = itemName;
        
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        this.itemDescription = itemDescription;

        this.quantity += quantity;
        if(this.quantity >= maxNumberOfItems)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
            isFull = true;
        
        //return the leftover
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }
        //update quantity
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;

       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void OnLeftClick()
    {
        if (thisItemSelected)
        { 
            inventoryManagers.UseItem(itemName);
        }
        inventoryManagers.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage.sprite == null)
            itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {

    }


    
    void Update()
    {
        
    }
}
