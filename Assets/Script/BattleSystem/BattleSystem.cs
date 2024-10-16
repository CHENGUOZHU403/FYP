using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{

    public TextMeshProUGUI EnemyHpText;
    public TextMeshProUGUI PlayerHpText;
   
    public int EnemyHp = 100 , PlayerHp = 100;

    
    public MCsystem MCsystem;

    public Text AccuracyText;
    public float Accuracy;
    public int Damage;

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
        AccuracyText.text = "Accuracy : " + Accuracy + "%";
    }

    public void TurnEnd() 
    {
        Damage = Mathf.RoundToInt(MCsystem.CorrectNum * 10 * Accuracy / 100);
        PlayerHp -= 20;
        EnemyHp -= Damage;

        PlayerHpText.text = "Player Hp: " + PlayerHp;
        EnemyHpText.text = "Enemy Hp:" + EnemyHp;
    }
}
