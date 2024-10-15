using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panelswitch : MonoBehaviour
{

    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;

    public void SwitchPanels()
    {
        if (InventoryMenu != null)
        {
            InventoryMenu.SetActive(false);
        }

        if (EquipmentMenu != null)
        {
            EquipmentMenu.SetActive(true);
        }
    }
}