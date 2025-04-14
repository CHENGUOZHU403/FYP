using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemFrame : MonoBehaviour
{
    public int price;
    public int quantity;
    public int ItemtotPricel;

    public Button subButton;
    public Button addButton;

    public TMP_Text ValueText;


    public ShopManager shopManager;

    [Header("Item Basic Value")]
    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        subButton.onClick.AddListener(SubItem);
        addButton.onClick.AddListener(AddItem);
    }

    void SubItem()
    {
        quantity -= 1;
        quantity = quantity < 0 ? 0 : quantity;
        UpdateTotalPrice();
    }

    void AddItem()
    {
        quantity += 1;
        quantity = quantity > 99 ? 99 : quantity;
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        if (ValueText == null  || shopManager == null)
    {
        Debug.LogError("有未赋值的字段！");
        return;
    }
        ItemtotPricel = quantity * price;
        ValueText.text = quantity.ToString();
        shopManager.UpdateTotalPrice();
    }
}
