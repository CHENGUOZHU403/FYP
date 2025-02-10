using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameObject EquipmentMenu;
    private bool menuActivated;

    private void Start()
    {
        menuActivated = EquipmentMenu.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("EquipmentMenu"))
        {
            menuActivated = !menuActivated;
            Time.timeScale = menuActivated ? 0 : 1;
            EquipmentMenu.SetActive(menuActivated);
        }
    }
}