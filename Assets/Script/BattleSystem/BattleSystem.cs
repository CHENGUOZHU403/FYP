using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
<<<<<<< Updated upstream
   // public enemyHp;
    // Start is called before the first frame update
    void Start()
=======
    public TextMeshProUGUI EnemyHpText;
    public TextMeshProUGUI PlayerHpText;
   
    public int EnemyHp = 100 , PlayerHp = 100;

    
    public MCsystem MCsystem;

    public Text AccuracyText;
    public float Accuracy;
    public float Damage;

    private void Update()
>>>>>>> Stashed changes
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
        Damage = MCsystem.CorrectNum * 10 * Accuracy / 100;
        PlayerHp -= 20;
        EnemyHp -= Mathf.RoundToInt(Damage);

        PlayerHpText.text = "Player Hp: " + PlayerHp;
        EnemyHpText.text = "Enemy Hp:" + EnemyHp;
    }
}
