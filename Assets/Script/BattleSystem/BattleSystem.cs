using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;
using System;

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


    public MCsystem MCsystem;
    public Timer Timer;


    public GameObject GameoverUI;
    public GameObject EnemyDamageGameObj;

    public Text GameOverTitle;

    public bool MonsterDead;

    private void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle()); 
    }

    IEnumerator SetupBattle()
    {

        dialogue.text = "Fight";

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.Playerturn;
        PlayerTurn();

    }



    IEnumerator PlayerAttack()
    {
        //damage the enmey
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogue.text = "Attack is successful";

        yield return new WaitForSeconds(2f);

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
            state = BattleState.Enemyturn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogue.text = enemyUnit.unitName + " Attack!";
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

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
        } else if (state == BattleState.Lost)
        {
            dialogue.text = "You were defeated.";
        }

    }

    void PlayerTurn()
    {
        dialogue.text = "Choose an action";

    }

    public void OnAttackButton()
    {
        if(state != BattleState.Playerturn)
            return;

        StartCoroutine(PlayerAttack());

    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogue.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.Enemyturn;
        StartCoroutine(EnemyTurn());
    }

    public void OnHealButton()
    {
        if (state != BattleState.Playerturn)
            return;

        StartCoroutine(PlayerHeal());
    }


    public void Reset()
    {
        Time.timeScale = 1;
        GameoverUI.SetActive(false);
        EnemyDamageGameObj.SetActive(true);
        Timer.Reset();

    }

    //public void TurnEnd() 
    //{
    //    PlayerDamage = Mathf.RoundToInt(MCsystem.CorrectNum * 10 * MCsystem.Accuracy / 100);
    //    palyerDamageText.text =  PlayerDamage.ToString();
    //    EnemyHp -= PlayerDamage;
    //    EnemyHpSlider.value = EnemyHp;
    //    EnemyHpText.text = "Enemy Hp:" + EnemyHp;
        
    //    if (!CheckWin())
    //    {
    //        EnemyDamage = Random.Range(15, 20);
    //        enemyDamageText.text = EnemyDamage.ToString();
    //        PlayerHp -= EnemyDamage;
    //        PlayerHpSlider.value = PlayerHp;
    //        PlayerHpText.text = "Player Hp: " + PlayerHp;
    //        CheckWin();
    //    }
    //}

    void PlyerAttack()
    {
        
    }

    void EnemyAttack()
    {

    }

    

    //bool CheckWin()
    //{
    //    if (PlayerHp <= 0)
    //    {
    //        Lose();
    //        PlayerHpText.text = "Player Hp: " + 0;
    //        return false;
    //    }
    //    if (EnemyHp <= 0)
    //    {
    //        EnemyDamageGameObj.SetActive(false);
    //        EnemyHpText.text = "Enemy Hp: " + 0;
    //        Win();
    //        return true;
    //    }
    //    return false;
    //}

    //void Lose()
    //{
    //    Time.timeScale = 0;
    //    GameOverTitle.text = "You Lose";
    //    GameoverUI.SetActive(true);
    //}

    //void Win()
    //{
    //    Time.timeScale = 0;
    //    GameOverTitle.text = "You Win";
    //    GameoverUI.SetActive(true);
    //    MonsterDead = true;
    //}

}
