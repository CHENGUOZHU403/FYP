using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text HPText;
    public TMP_Text levelText;      

    public TMP_Text moneyText;
    private HeroKnight player;     
    public PlayerData PlayerData;



    void Start()
    {
        //  find Player
        player = FindObjectOfType<HeroKnight>();

        if (player == null)
        {
            Debug.LogError("HeroKnight script not found in the scene!");
        }

        // 檢查是否分配了 PlayerData
        if (PlayerData == null)
        {
            Debug.LogError("PlayerData reference is missing!");
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        HPText.text = "Hp : " + PlayerData.currentHealth.ToString();

        levelText.text = "Level: " + player.level;

        moneyText.text = player.money.ToString();

        float xpProgress = (float)player.currentXP / player.xpToLevelUp;
    }
}
