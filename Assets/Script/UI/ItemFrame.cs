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
        ValueText.text = "Value : " + quantity;
        UpdateTotalPrice();
    }

    void AddItem()
    {
        quantity += 1;
        ValueText.text = "Value : " + quantity;
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        ItemtotPricel = quantity * price;
        shopManager.UpdateTotalPrice();
    }
}
