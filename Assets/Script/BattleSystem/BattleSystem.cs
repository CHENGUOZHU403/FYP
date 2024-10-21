using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{

    public TextMeshProUGUI EnemyHpText;
    public TextMeshProUGUI PlayerHpText;
    public Text palyerDamageText;
    public Text enmyDamageText;

   
    public int EnemyHp = 100 , PlayerHp = 100;

    
    public MCsystem MCsystem;
    public Timer Timer;

    public Text AccuracyText;
    public float Accuracy;
    public int PlayerDamage, EnemyDamage;

    public GameObject GameoverUI;

    public Text GameOverTitle;

    private void Update()
    {
        if (MCsystem.Answered == 0)
        {
            Accuracy = 0;
        }
        else
        {
            Accuracy = 1.0f * MCsystem.CorrectNum / MCsystem.Answered * 100;
        }
        AccuracyText.text = "Accuracy : " + Mathf.RoundToInt(Accuracy) + "%";



    }

    public void Reset()
    {
        EnemyHp = 100;
        PlayerHp = 100;
        PlayerHpText.text = "Player Hp: " + PlayerHp;
        EnemyHpText.text = "Enemy Hp:" + EnemyHp;
        Time.timeScale = 1;
        GameoverUI.SetActive(false);
        Timer.Reset();

    }

    public void TurnEnd() 
    {
        PlayerDamage = Mathf.RoundToInt(MCsystem.CorrectNum * 10 * Accuracy / 100);
        palyerDamageText.text =  PlayerDamage.ToString();
        EnemyHp -= PlayerDamage;
        if (!CheckWin())
        {
            EnemyDamage = Random.Range(15, 20);
            enmyDamageText.text = EnemyDamage.ToString();
            PlayerHp -= EnemyDamage;
            CheckWin();
        }

        PlayerHpText.text = "Player Hp: " + PlayerHp;
        EnemyHpText.text = "Enemy Hp:" + EnemyHp;
    }

    bool CheckWin()
    {
        if (PlayerHp <= 0)
        {
            Lose();
            return false;
        }
        if (EnemyHp <= 0)
        {
            
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
    }

}
