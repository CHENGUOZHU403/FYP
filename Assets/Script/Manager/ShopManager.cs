using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TMP_Text UserMoneyText;

    public ItemFrame[] itemFrameslist;

    public TMP_Text totalPriceText;

    public TMP_Text alertText;

    public Button buyButton;

    private int totalPrice = 0;

    public PlayerData playerData;

    //public GameObject shopUi;

    private InventoryManagers inventoryManagers;

    void Start()
    {
        buyButton.onClick.AddListener(PurchaseItems);
        UserMoneyText.text = playerData.money.ToString();
        inventoryManagers = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagers>();
    }

    void Update()
    {
        UpdatePlayerMoneyUI();
    }

    void UpdatePlayerMoneyUI()
    {
        UserMoneyText.text = playerData.money.ToString();
    }

    void PurchaseItems()
    {
        if(totalPrice == 0)
        {
            alertText.text = "You can't buy anything!";
        }

        else if (totalPrice <= playerData.money) 
        {
            foreach (var itemFrame in itemFrameslist)
            {
                if(itemFrame.quantity > 0)
                {
                    inventoryManagers.AddItem(
                        itemFrame.itemName,
                        itemFrame.quantity,
                        itemFrame.itemSprite,
                        itemFrame.itemDescription,
                        itemFrame.itemType
                    );
                }
            }

            Debug.Log("Player purchased item(s)");
            alertText.text = "Purchase successfully!";
    
            playerData.money -= totalPrice;
            UpdatePlayerMoneyUI();
            Reset();
        }
        else
        {
            Debug.Log("You have not enough money");
            alertText.text = "Not enough money!";
        }

        StartCoroutine(ClearAlert());
    }

    public void UpdateTotalPrice()
    {
        totalPrice = 0;
        foreach (var item in itemFrameslist)
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
            item.ValueText.text = "0";
            item.ItemtotPricel = 0;
        }

        totalPrice = 0;
        totalPriceText.text = "Total Price : " + totalPrice;

    }

    private IEnumerator ClearAlert()
    {
        yield return new WaitForSeconds(2f);
        alertText.text = "";
    }

    // public void hideShopUI()
    // {
    //     shopUi.SetActive(false);
    // }
}
