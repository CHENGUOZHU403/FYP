using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleHUD : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Slider hpSlider;
    public TMP_Text hpText;

    [Header("Character Image")]
    public Transform imageTransform;

    private int maxHealth;

    private void SetCommonHUD(string name, int maxHealth, int level, Sprite sprite)
    {
        nameText.text = name;

        this.maxHealth = maxHealth;

        levelText.text = $"Lvl {level}";

        var imageComponent = imageTransform.GetComponentInChildren<SpriteRenderer>();
        if (imageComponent != null && sprite != null)
        {
            imageComponent.sprite = sprite;
        }

        UpdateHealth(hpSlider.value, maxHealth);
    }

    public void SetHUD(MonsterData monsterData)
    {
        SetCommonHUD(monsterData.monsterName, monsterData.maxHealth, monsterData.level , monsterData.monsterSprite);
        hpSlider.maxValue = monsterData.maxHealth;
        hpSlider.value = monsterData.maxHealth;
    }

    public void SetHUD(PlayerData playerData)
    {
        SetCommonHUD(playerData.playerName, playerData.maxHealth, playerData.level, playerData.PlayerImage);

        // 更新滑块和血量
        hpSlider.maxValue = playerData.maxHealth;
        hpSlider.value = playerData.currentHealth;
    }

    public void SetHP(int currentHealth)
    {
        UpdateHealth(currentHealth, maxHealth);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        hpText.text = $"{Mathf.CeilToInt(currentHealth)} / {Mathf.CeilToInt(maxHealth)}";
        hpText.text = $"{currentHealth}/{maxHealth}";
        hpSlider.value = currentHealth;
    }

}
