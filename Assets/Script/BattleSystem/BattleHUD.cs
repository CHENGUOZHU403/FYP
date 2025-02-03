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

    public Animator animator;

    private void SetCommonHUD(string name, int maxHealth, int level, GameObject prefab, Sprite sprite)
    {
        nameText.text = name;

        this.maxHealth = maxHealth;

        levelText.text = $"Lvl {level}";

        var imageComponent = imageTransform.GetComponentInChildren<SpriteRenderer>();
        if (imageComponent != null && sprite != null)
        {
            imageComponent.sprite = sprite;
        }

        GameObject GO = Instantiate(prefab, imageTransform);

        animator = GO.GetComponent<Animator>();

        UpdateHealth(hpSlider.value, maxHealth);
    }

    public void SetHUD(MonsterData monsterData)
    {
        SetCommonHUD(monsterData.monsterName, monsterData.maxHealth, monsterData.level ,monsterData.monsterPrefab, monsterData.monsterSprite);
        hpSlider.maxValue = monsterData.maxHealth;
        hpSlider.value = monsterData.maxHealth;
        //animator = monsterData.monsterPrefab.GetComponent<Animator>();
    }

    public void SetHUD(PlayerData playerData)
    {
        SetCommonHUD(playerData.playerName, playerData.maxHealth, playerData.level,playerData.playerPrefab, playerData.PlayerImage);
        hpSlider.maxValue = playerData.maxHealth;
        hpSlider.value = playerData.currentHealth;
        //animator = playerData.playerPrefab.GetComponent<Animator>();
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

    public IEnumerator Move(Vector3 targetPosition)
    {
        float duration = 1f; // Duration of the movement
        float elapsed = 0f;
        Vector3 startingPosition = imageTransform.position;
        Vector3 stoppingPosition = targetPosition;

        animator.SetBool("isMove", true);

        while (elapsed < duration)
        {   
            imageTransform.position = Vector3.Lerp(startingPosition, stoppingPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        // Ensure the unit ends exactly at the target position
        imageTransform.position = stoppingPosition;
        animator.SetBool("isMove", false);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

}
