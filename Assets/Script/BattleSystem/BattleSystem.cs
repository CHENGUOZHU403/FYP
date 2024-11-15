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

        yield return new WaitForSeconds(2f);

        state = BattleState.Playerturn;
        PlayerTurn();

    }

    IEnumerator EnemyTurn()
    {
        dialogue.text = enemyUnit.unitName + " Attack!";
        yield return new WaitForSeconds(1f);

        int EnemyDamage = Random.Range(enemyUnit.damage-5, enemyUnit.damage+5);
        enemyDamageText.text = EnemyDamage.ToString();
        UiManager.ShowEnemyDamage();
        bool isDead = playerUnit.TakeDamage(EnemyDamage);

        playerHUD.SetHP(playerUnit.currentHP);

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
        UiManager.hideDamage();
        UiManager.ChooseAction();
        
        dialogue.text = "Choose an action";

    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(6f);


        int playerDamage = Mathf.RoundToInt(MCsystem.CorrectNum * playerUnit.damage * MCsystem.Accuracy / 100);
        //damage to enmey

        palyerDamageText.text = playerDamage.ToString();
        UiManager.ShowPlayerDamage();
      
        bool isDead = enemyUnit.TakeDamage(playerDamage);
        

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogue.text = "Attack is successful";

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
        playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogue.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.Playerturn)
            return;


        UiManager.Attack();
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

    void PlyerAttack()
    {
        
    }

    void EnemyAttack()
    {

    }
}
