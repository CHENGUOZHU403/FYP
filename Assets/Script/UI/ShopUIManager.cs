using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopPanel; 
    public Button openShopButton; 

    private bool isShopOpen = false; 

    private void Start()
    {
        shopPanel.SetActive(false);

        openShopButton.onClick.AddListener(ToggleShopUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isShopOpen)
        {
            CloseShopUI();
        }
    }

    public void ToggleShopUI()
    {
        if (isShopOpen)
        {
            CloseShopUI();
        }
        else
        {
            OpenShopUI();
        }
    }

    // 打開商店 UI
    private void OpenShopUI()
    {
        shopPanel.SetActive(true);
        isShopOpen = true;
        Time.timeScale = 0f; 
    }

    // 關閉商店 UI
    private void CloseShopUI()
    {
        shopPanel.SetActive(false);
        isShopOpen = false;
        Time.timeScale = 1f;
    }
}
