using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    public Slider PlayerHpSlider;
    public Slider EnemyHpSlider;

    public TextMeshProUGUI EnemyHpText;
    public TextMeshProUGUI PlayerHpText;
    public Text palyerDamageText;
    public Text enemyDamageText;

    public GameObject player;
    public GameObject enemy;


    public int EnemyHp, PlayerHp;
    public int PlayerDamage, EnemyDamage;

    public MCsystem MCsystem;
    public Timer Timer;

    public GameObject GameoverUI;
    public GameObject EnemyDamageGameObj;

    public Text GameOverTitle;

    public bool MonsterDead;

    private void Start()
    {
        EnemyHp = 100;
        PlayerHp = 100;
        PlayerHpSlider.maxValue = PlayerHp;
        PlayerHpSlider.value = PlayerHp;
        EnemyHpSlider.maxValue = EnemyHp;
        EnemyHpSlider.value = EnemyHp;
    }



    public void Reset()
    {
        EnemyHp = 100;
        PlayerHp = 100;
        PlayerHpText.text = "Player Hp: " + PlayerHp;
        EnemyHpText.text = "Enemy Hp: " + EnemyHp;
        PlayerHpSlider.maxValue = PlayerHp;
        PlayerHpSlider.value = PlayerHp;
        EnemyHpSlider.maxValue = EnemyHp;
        EnemyHpSlider.value = EnemyHp;
        Time.timeScale = 1;
        GameoverUI.SetActive(false);
        EnemyDamageGameObj.SetActive(true);
        Timer.Reset();

    }

    public void TurnEnd() 
    {
        PlayerDamage = Mathf.RoundToInt(MCsystem.CorrectNum * 10 * MCsystem.Accuracy / 100);
        palyerDamageText.text =  PlayerDamage.ToString();
        EnemyHp -= PlayerDamage;
        EnemyHpSlider.value = EnemyHp;
        EnemyHpText.text = "Enemy Hp:" + EnemyHp;
        
        if (!CheckWin())
        {
            EnemyDamage = Random.Range(15, 20);
            enemyDamageText.text = EnemyDamage.ToString();
            PlayerHp -= EnemyDamage;
            PlayerHpSlider.value = PlayerHp;
            PlayerHpText.text = "Player Hp: " + PlayerHp;
            CheckWin();
        }
    }

    void PlyerAttack()
    {
        
    }

    void EnemyAttack()
    {

    }

    

    bool CheckWin()
    {
        if (PlayerHp <= 0)
        {
            Lose();
            PlayerHpText.text = "Player Hp: " + 0;
            return false;
        }
        if (EnemyHp <= 0)
        {
            EnemyDamageGameObj.SetActive(false);
            EnemyHpText.text = "Enemy Hp: " + 0;
            Win();
            return true;
        }
        return false;
    }

    void Lose()
    {
        Time.timeScale = 0;
        GameOverTitle.text = "You Lose";
        GameoverUI.SetActive(true);
    }

    void Win()
    {
        Time.timeScale = 0;
        GameOverTitle.text = "You Win";
        GameoverUI.SetActive(true);
        MonsterDead = true;
    }

}
