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

    public float attackRange;

    private void SetCommonHUD(string name, int maxHealth, int level, GameObject prefab, float attackRange)
    {
        nameText.text = name;

        this.maxHealth = maxHealth;

        levelText.text = $"Lvl {level}";

        var imageComponent = imageTransform.GetComponentInChildren<SpriteRenderer>();

        GameObject GO = Instantiate(prefab, imageTransform);

        animator = GO.GetComponent<Animator>();

        UpdateHealth(hpSlider.value, maxHealth);

        this.attackRange = attackRange;
    }

    public void SetHUD(MonsterData monsterData)
    {
        SetCommonHUD(monsterData.monsterName, monsterData.maxHealth, monsterData.level ,monsterData.monsterPrefab, monsterData.attackRange);
        hpSlider.maxValue = monsterData.maxHealth;
        hpSlider.value = monsterData.maxHealth;
        attackRange = monsterData.attackRange;
    }

    public void SetHUD(PlayerData playerData)
    {
        SetCommonHUD(playerData.playerName, playerData.maxHealth, playerData.level,playerData.playerPrefab, playerData.attackRange );
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

    public IEnumerator Move(Vector3 targetPosition)
    {
        float duration = 1f; // Duration of the movement
        float elapsed = 0f;
        Vector3 startingPosition = imageTransform.position;
        Vector3 stoppingPosition = targetPosition - new Vector3(attackRange,0,0);

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

    public IEnumerator MonsterMove(Vector3 targetPosition)
    {
        float duration = 1f; // Duration of the movement
        float elapsed = 0f;
        Vector3 startingPosition = imageTransform.position;
        imageTransform.transform.eulerAngles = new Vector3(
        imageTransform.transform.eulerAngles.x,
        imageTransform.transform.eulerAngles.y + 180,
        imageTransform.transform.eulerAngles.z
        );

        Vector3 stoppingPosition = targetPosition - new Vector3(attackRange, 0, 0);

        animator.SetBool("isMove", true);

        while (elapsed < duration)
        {
            imageTransform.position = Vector3.Lerp(startingPosition, stoppingPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        // Ensure the unit ends exactly at the target position
        imageTransform.position = stoppingPosition;
        imageTransform.transform.eulerAngles = new Vector3(
        imageTransform.transform.eulerAngles.x,
        imageTransform.transform.eulerAngles.y - 180,
        imageTransform.transform.eulerAngles.z
        );
        animator.SetBool("isMove", false);
    }

    public IEnumerator Back(Vector3 targetPosition)
    {
        float duration = 1f; // Duration of the movement
        float elapsed = 0f;
        Vector3 startingPosition = imageTransform.position;
        imageTransform.transform.eulerAngles = new Vector3(
        imageTransform.transform.eulerAngles.x,
        imageTransform.transform.eulerAngles.y + 180,
        imageTransform.transform.eulerAngles.z
        );
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
        imageTransform.transform.eulerAngles = new Vector3(
        imageTransform.transform.eulerAngles.x,
        imageTransform.transform.eulerAngles.y - 180,
        imageTransform.transform.eulerAngles.z
        );
        animator.SetBool("isMove", false);

    }

    public IEnumerator MonsterBack(Vector3 targetPosition)
    {
        float duration = 1f; // Duration of the movement
        float elapsed = 0f;
        Vector3 startingPosition = imageTransform.position;
        imageTransform.transform.eulerAngles = new Vector3(
        imageTransform.transform.eulerAngles.x,
        imageTransform.transform.eulerAngles.y,
        imageTransform.transform.eulerAngles.z
        );
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
        imageTransform.transform.eulerAngles = new Vector3(
        imageTransform.transform.eulerAngles.x,
        imageTransform.transform.eulerAngles.y,
        imageTransform.transform.eulerAngles.z
        );
        animator.SetBool("isMove", false);

    }

    public void Attack()
    {
        animator.SetTrigger("Attack1");
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

}
