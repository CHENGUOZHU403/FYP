using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text HPText;
    public TMP_Text levelText;        // 用來顯示等級的 Text 元件

    public TMP_Text moneyText;
    private HeroKnight player;     // 參考玩家的 HeroKnight 腳本
    public PlayerData PlayerData;



    void Start()
    {
        // 找到場景中的 HeroKnight（玩家）腳本
        player = FindObjectOfType<HeroKnight>();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        HPText.text = "Hp : " + PlayerData.currentHealth.ToString();

        // 更新等級文字
        levelText.text = "Level: " + player.level;

        moneyText.text = player.money.ToString();

        // 計算經驗進度比例
        float xpProgress = (float)player.currentXP / player.xpToLevelUp;
    }
}
