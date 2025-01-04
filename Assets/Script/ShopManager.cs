using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int userMoney = 100; 
    public int itemPrice = 20;   
    public TMP_Text totalPriceText;  
    public TMP_Text itemPriceText;   
    public Button addButton;      
    public Button buyButton;     

    private int totalPrice = 0;   

    void Start()
    {

        addButton.onClick.AddListener(AddItem);
        buyButton.onClick.AddListener(PurchaseItems);
    }

    void AddItem()
    {
        totalPrice += itemPrice;
        totalPriceText.text = "Total price: " + totalPrice;
    }

    void PurchaseItems()
    {
        if (totalPrice <= userMoney)
        {
            Debug.Log("Player purchased item(s)");
            // ?????????????
            totalPrice = 0; // ?????
            totalPriceText.text = "PLayer used money: " + totalPrice;
        }
        else
        {
            Debug.Log("You have not enought money");

        }
    }
}
