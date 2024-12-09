using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject shopPanel; 

    private bool isShopOpen = false; 

    private void Start()
    {
        shopPanel.SetActive(false);
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

    public void OpenShopUI()
    {
        shopPanel.SetActive(true); 
        isShopOpen = true;
        Time.timeScale = 0f; 
    }

    public void CloseShopUI()
    {
        shopPanel.SetActive(false); 
        isShopOpen = false;
        Time.timeScale = 1f; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isShopOpen)
        {
            CloseShopUI();
        }
    }
}