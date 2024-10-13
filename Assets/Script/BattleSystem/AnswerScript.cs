using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public BattleSystem BattleSystem;
    public void Answer() {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            BattleSystem.correct();
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
    }
}
