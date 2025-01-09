using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquippedSlot : MonoBehaviour
{
    //Slot appearanvce
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TMP_Text slotName;

    //slot data
    [SerializeField]
    private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    //other variables
    private bool slotInUse;

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        //update image
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        //update data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        slotInUse = true;
    }
}
