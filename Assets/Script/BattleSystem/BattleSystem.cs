using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;



public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public TextMeshProUGUI dialogue;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    unit playerUnit;
    unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public DamageDisplay damageDisplay;

    public UiManager UiManager;
    public MCsystem MCsystem;
    public Timer Timer;

    

    private void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<unit>();

        

        //playerHUD.SetHUD(playerUnit);
        //enemyHUD.SetHUD(enemyUnit);

        yield return StartCoroutine(ShowDialogue("Welcome to Math Wrold"));

        yield return new WaitForSeconds(1f);
        state = BattleState.Playerturn;
        PlayerTurn();

    }

    IEnumerator EnemyTurn()
    {
        Vector3 originalPosition = enemyUnit.transform.position;

        //dialogue.text = enemyUnit.unitName + " Attack!";
        StartCoroutine(ShowDialogue(enemyUnit.unitName + " Attack!"));
        yield return StartCoroutine(enemyUnit.Move(enemyUnit.transform, playerBattleStation.position, enemyUnit.attackRange));

        enemyUnit.Attack();
        playerUnit.Hurt();

        int EnemyDamage = Random.Range(enemyUnit.damage-5, enemyUnit.damage+5);

        damageDisplay.ShowDamage(playerUnit.transform.position, EnemyDamage, enemyUnit.attackRange);
        UiManager.ShowDamage();
        bool isDead = playerUnit.TakeDamage(EnemyDamage);
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(enemyUnit.Move(enemyUnit.transform, originalPosition, 0));

        yield return new WaitForSeconds(1f);

        
        if (isDead)
        {
            // End the battle
            playerUnit.Dead();
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            //Player Turn
            state = BattleState.Playerturn;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.Won)
        {
            dialogue.text = "You won the battle!";
            UiManager.GameOver("You won the battle!");

        } else if (state == BattleState.Lost)
        {
            dialogue.text = "You were defeated.";
            UiManager.GameOver("You were defeated.");
        }

    }

    void PlayerTurn()
    {
        StartCoroutine(ShowDialogue("Choose your action"));
        UiManager.ChooseAction();
    }

    IEnumerator PlayerAttack()
    {
        
        yield return new WaitForSeconds(Timer.timerDuration);

        Vector3 originalPosition = playerUnit.transform.position;

        //dialogue.text = "Attack is successful";
        StartCoroutine(ShowDialogue("Attack is successful"));
        UiManager.ShowDialogue();

        yield return StartCoroutine(playerUnit.Move(playerUnit.transform, enemyBattleStation.position, playerUnit.attackRange)); 

        playerUnit.Attack();
        enemyUnit.Hurt();

        int playerDamage = Mathf.RoundToInt(MCsystem.correctCount * playerUnit.damage * MCsystem.accuracy / 100);
        damageDisplay.ShowDamage(enemyUnit.transform.position, playerDamage, playerUnit.attackRange);
        UiManager.ShowDamage();

        bool isDead = enemyUnit.TakeDamage(playerDamage);
        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(playerUnit.Move(playerUnit.transform, originalPosition, 0));

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            enemyUnit.Dead();
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(10);
        playerHUD.SetHP(playerUnit.currentHP);
        yield return StartCoroutine(ShowDialogue("You feel renewed strength!"));
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.Playerturn)
            return;

        Timer.ResetTimer();
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.Playerturn)
            return;

        state = BattleState.Enemyturn;
        StartCoroutine(PlayerHeal());
    }

    IEnumerator ShowDialogue(string sentence)
    {
        dialogue.text = "";
        foreach (char c in sentence)
        {
            dialogue.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator Move(Transform unitTransform, Vector3 targetPosition, float attackRange)
    {
        float duration = 1f; // Duration of the movement
        float elapsed = 0f;

        Vector3 startingPosition = unitTransform.position;
        Vector3 stoppingPosition = new Vector3(targetPosition.x - attackRange, targetPosition.y, targetPosition.z);


        while (elapsed < duration)
        {
            unitTransform.position = Vector3.Lerp(startingPosition, stoppingPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the unit ends exactly at the target position
        unitTransform.position = stoppingPosition;
    }
}
