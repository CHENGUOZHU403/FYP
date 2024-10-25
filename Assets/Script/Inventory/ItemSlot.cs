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

    public void AddItem(string itemName, int quantity, Sprite itemSprite,string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
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
