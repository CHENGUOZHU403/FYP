using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text HPText;
    public Slider HpSilder;

    public TMP_Text levelText;
    public Slider LevelSilder;

    public TMP_Text moneyText;

    private HeroKnight player;     



    void Start()
    {
        //  find Player
        player = FindObjectOfType<HeroKnight>();

        if (player == null)
        {
            Debug.LogError("HeroKnight script not found in the scene!");
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        HpSilder.maxValue = player.playerData.maxHealth;
        HpSilder.value = player.playerData.currentHealth;
        HPText.text = "Hp : " + player.playerData.currentHealth.ToString();

        LevelSilder.maxValue = player.playerData.xpToNextLevel;
        LevelSilder.value = player.playerData.currentXP;
        levelText.text = "Level : " + player.playerData.level;

        moneyText.text = player.money.ToString();

        float xpProgress = (float)player.currentXP / player.xpToLevelUp;
    }
}
