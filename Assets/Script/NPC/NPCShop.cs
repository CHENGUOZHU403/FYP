using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public int itemCost = 20;

    public void BuyItem(HeroKnight player)
    {
        if (player.SpendMoney(itemCost))
        {
            Debug.Log("Item purchased!");
            // Add item to player's inventory
        }
        else
        {
            Debug.Log("You cannot buy this item.");
        }
    }


}
