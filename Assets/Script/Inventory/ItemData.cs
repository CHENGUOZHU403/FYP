using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public ItemType itemType;
    public string itemName;
    public int quantity;
    public Sprite sprite;

    public void DebugItem()
    {
        Debug.Log("itemType: "+ itemType+ " itemName: "+ itemName+ " quantity:"+ quantity+ " sprite: "+ sprite);
    }
}
