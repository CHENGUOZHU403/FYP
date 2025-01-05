using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int userMoney = 100;
    public TMP_Text UserMoneyText;

    public ItemFrame[] itemFrameslist;

    public TMP_Text totalPriceText;

    public TMP_Text alartText;

    public Button buyButton;

    private int totalPrice = 0;   

    void Start()
    {
        buyButton.onClick.AddListener(PurchaseItems);
        UserMoneyText.text = userMoney.ToString();
    }

    void PurchaseItems()
    {
        if (totalPrice <= userMoney)
        {
            Debug.Log("Player purchased item(s)");
            alartText.text = "Purchase successfully";
            userMoney -= totalPrice;
            UserMoneyText.text = userMoney.ToString();
            Reset();
        }
        else
        {
            Debug.Log("You have not enought money");
            alartText.text = "Not Enought Money";
        }
    }

    public void UpdateTotalPrice()
    {
        totalPrice = 0;
        foreach(var item in itemFrameslist)
        {
            totalPrice += item.ItemtotPricel;
        }
        totalPriceText.text = "Total Price : " + totalPrice;
    }

    private void Reset()
    {
        foreach (var item in itemFrameslist)
        {
            item.quantity = 0;
            item.ValueText.text = "Value : 0";
            item.ItemtotPricel = 0;
        }

        totalPrice = 0;
        totalPriceText.text = "Total Price : " + totalPrice;

    }
}
