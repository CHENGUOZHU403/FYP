using System.Collections;
using UnityEngine;
using TMPro;

public enum BattleState { Start, Playerturn, Enemyturn, Won, Lost }


public class BattleManager : MonoBehaviour
{
    public BattleState state;

    [Header("Monster Data")]
    public MonsterData[] allMonsters;
    public int monsterCurrentHealth;
    public MonsterData encounteredMonster;

    [Header("Player Data")]
    public PlayerData playerData;

    [Header("UI Elements")]
    public BattleHUD playerHUD;
    public BattleHUD monsterHUD;
    public TMP_Text dialogueText;

    public DamageDisplay damageDisplay;

    public UiManager uiManager;
    public MCsystem MCsystem;
    public Timer timer;

    void Start()
    {
        string monsterName = PlayerPrefs.GetString("EncounteredMonster", "");
        encounteredMonster = System.Array.Find(allMonsters, m => m.name == monsterName);

        if (encounteredMonster != null)
        {
            StartCoroutine(InitializeBattle());
        }
        else
        {
            Debug.LogError("Encountered monster not found!");
        }
    }

    IEnumerator InitializeBattle()
    {
        monsterCurrentHealth = encounteredMonster.maxHealth;
        monsterHUD.SetHUD(encounteredMonster);


        playerHUD.SetHUD(playerData);
        UpdateUI();

        yield return StartCoroutine(ShowDialogue("You meet " + encounteredMonster.monsterName + "!"));
        yield return new WaitForSeconds(1f);

        state = BattleState.Playerturn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        StartCoroutine(ShowDialogue("Choose your action"));
        uiManager.ChooseAction();
    }

    public void OnAttackButton()
    {
        if (state != BattleState.Playerturn)
            return;

        timer.ResetTimer();
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.Playerturn)
            return;

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerAttack()
    {
        uiManager.Attack();
        yield return new WaitForSeconds(timer.timerDuration);

        uiManager.HideMultChoiUI();

        Vector3 originalPosition = playerHUD.imageTransform.position;

        //yield return StartCoroutine(playerUnit.Move(playerUnit.transform, enemyBattleStation.position, playerUnit.attackRange));
        yield return StartCoroutine(playerHUD.Move(monsterHUD.imageTransform.position));

        playerHUD.Attack();
        monsterHUD.Hurt();

        uiManager.ShowDamage();

        int playerDamage = Mathf.RoundToInt(playerData.attackPower * MCsystem.correctCount * MCsystem.accuracy / 100 * 0.5f ) ;
        //int playerDamage = Mathf.RoundToInt(playerData.attackPower * Random.Range(0.8f, 1.2f));
        damageDisplay.ShowDamage(monsterHUD.imageTransform.position, playerDamage, 1.5f);
        monsterCurrentHealth -= playerDamage;

        monsterHUD.SetHP(monsterCurrentHealth);

        yield return StartCoroutine(ShowDialogue($"You dealt {playerDamage} damage!"));



        yield return StartCoroutine(playerHUD.Move(originalPosition));


        if (monsterCurrentHealth <= 0)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.Enemyturn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerData.Heal(10);
        playerHUD.SetHP(playerData.currentHealth);
        yield return StartCoroutine(ShowDialogue("You feel renewed strength!"));
        StartCoroutine(EnemyTurn());
    }


    IEnumerator EnemyTurn()
    {
        Vector3 originalPosition = monsterHUD.imageTransform.position;

        yield return StartCoroutine(ShowDialogue($"{encounteredMonster.monsterName} attacks!"));
        yield return StartCoroutine(monsterHUD.Move(playerHUD.imageTransform.position));

        monsterHUD.Attack();
        playerHUD.Hurt();

        int damage = Mathf.RoundToInt(encounteredMonster.attackPower * Random.Range(0.8f, 1.2f));
        damageDisplay.ShowDamage(playerHUD.imageTransform.position, damage, -1.5f);
        playerData.TakeDamage(damage);
        playerHUD.SetHP(playerData.currentHealth);
        uiManager.ShowDamage();
        playerHUD.SetHP(playerData.currentHealth);



        yield return StartCoroutine(monsterHUD.Move(originalPosition));


        if (playerData.currentHealth <= 0)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.Playerturn;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        string monsterID = PlayerPrefs.GetString("CurrentMonster", "");

        if (!string.IsNullOrEmpty(monsterID))
        {
            GameManager.Instance.MarkMonsterAsDefeated(monsterID);
        }

        if (state == BattleState.Won)
        {
            StartCoroutine(ShowDialogue("You won the battle!"));
            uiManager.GameOver("Victory");
        }
        else if (state == BattleState.Lost)
        {
            StartCoroutine(ShowDialogue("You were defeated..."));
            uiManager.GameOver("Defeat");
        }
    }

    IEnumerator ShowDialogue(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
    }

    void UpdateUI()
    {
        playerHUD.UpdateHealth(playerData.currentHealth, playerData.maxHealth);
        monsterHUD.UpdateHealth(monsterCurrentHealth, encounteredMonster.maxHealth);
    }
}

