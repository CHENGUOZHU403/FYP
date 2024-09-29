using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSecondUI : MonoBehaviour
{
    public GameObject SettingMenu;
 

    private void Start()
    {
        // 在 UI 按鈕上加入監聽器
        SettingMenu.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(ShowSettingsPanelOnClick);
    }

    public void ShowSettingsPanelOnClick()
    {
        SettingMenu.SetActive(true);
    }
}