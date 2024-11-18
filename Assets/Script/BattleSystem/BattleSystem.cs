using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { Start, Playerturn, Enemyturn, Won, Lost }

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

    public Text palyerDamageText;
    public Text enemyDamageText;

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

        dialogue.text = "Fight";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleState.Playerturn;
        PlayerTurn();

    }

    IEnumerator EnemyTurn()
    {
        Vector3 originalPosition = enemyUnit.transform.position;

        dialogue.text = enemyUnit.unitName + " Attack!";
        yield return StartCoroutine(Move(enemyUnit.transform, playerUnit.transform.position, enemyUnit.attackRange, enemyUnit));

        enemyUnit.Attack();

        int EnemyDamage = Random.Range(enemyUnit.damage-5, enemyUnit.damage+5);

        damageDisplay.ShowDamage(playerUnit.transform.position, EnemyDamage, enemyUnit.attackRange);
        UiManager.ShowDamage();
        bool isDead = playerUnit.TakeDamage(EnemyDamage);
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Move(enemyUnit.transform, originalPosition, 0, enemyUnit));

        yield return new WaitForSeconds(1f);

        
        if (isDead)
        {
            // End the battle
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
            UiManager.Gameover("You won the battle!");

        } else if (state == BattleState.Lost)
        {
            dialogue.text = "You were defeated.";
            UiManager.Gameover("You were defeated.");
        }

    }

    void PlayerTurn()
    {
        UiManager.ChooseAction();
        dialogue.text = "Choose an action";
    }

    IEnumerator PlayerAttack()
    {
        
        yield return new WaitForSeconds(Timer.timerDuration);

        Vector3 originalPosition = playerUnit.transform.position;

        dialogue.text = "Attack is successful";
        UiManager.ShowDialogue();
        

        yield return StartCoroutine(Move(playerUnit.transform, enemyUnit.transform.position, playerUnit.attackRange, playerUnit));

        playerUnit.Attack();

        int playerDamage = Mathf.RoundToInt(MCsystem.CorrectNum * playerUnit.damage * MCsystem.Accuracy / 100);
    
        damageDisplay.ShowDamage(enemyUnit.transform.position, playerDamage, playerUnit.attackRange);
        UiManager.ShowDamage();
        bool isDead = enemyUnit.TakeDamage(playerDamage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Move(playerUnit.transform, originalPosition, 0, playerUnit));

        yield return new WaitForSeconds(1f);

        

        //Check if the enmey is dead
        //change state 
        if (isDead)
        {
            // End the battle
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            //Enemy Turn
            Debug.Log("enemy turn");
            
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(10);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogue.text = "You feel renewed strength!";

        yield return new WaitForSeconds(1f);
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

    IEnumerator Move(Transform unitTransform, Vector3 targetPosition, float attackRange, unit actionUnit)
    {
        float duration = 0.5f; // Duration of the movement
        float elapsed = 0f;

        Vector3 startingPosition = unitTransform.position;
        Vector3 stoppingPosition = new Vector3(targetPosition.x - attackRange, targetPosition.y, targetPosition.z);

        actionUnit.setWalkingBool(true);

        while (elapsed < duration)
        {
            unitTransform.position = Vector3.Lerp(startingPosition, stoppingPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the unit ends exactly at the target position
        unitTransform.position = stoppingPosition;
        actionUnit.setWalkingBool(false);
    }

}
