using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int userMoney = 100; // ????? 100 ??
    public int itemPrice = 20;   // ????
    public Text totalPriceText;  // ???? UI ??
    public Text itemPriceText;   // ????? UI ??
    public Button addButton;      // ???????
    public Button buyButton;      // ????

    private int totalPrice = 0;   // ???

    void Start()
    {
        itemPriceText.text = "????: " + itemPrice + " ??";
        totalPriceText.text = "???: " + totalPrice;

        addButton.onClick.AddListener(AddItem);
        buyButton.onClick.AddListener(PurchaseItems);
    }

    void AddItem()
    {
        totalPrice += itemPrice;
        totalPriceText.text = "???: " + totalPrice;
    }

    void PurchaseItems()
    {
        if (totalPrice <= userMoney)
        {
            Debug.Log("?????");
            // ?????????????
            totalPrice = 0; // ?????
            totalPriceText.text = "???: " + totalPrice;
        }
        else
        {
            Debug.Log("???????????");
            // ?????????????
        }
    }
}
