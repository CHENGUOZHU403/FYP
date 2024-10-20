using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public MCsystem MCSystem;
    public void Answer() {
        if (isCorrect)
        {
            MCSystem.correct();
        }
        else
        {
            MCSystem.wrong();
        }
    }
}
