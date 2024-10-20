using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSecondUI : MonoBehaviour
{
    public GameObject SettingMenu;
 

    void Start()
    {
        SettingMenu.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(ShowSettingsPanelOnClick);
    }

    public void ShowSettingsPanelOnClick()
    {
        SettingMenu.SetActive(true);
    }


}