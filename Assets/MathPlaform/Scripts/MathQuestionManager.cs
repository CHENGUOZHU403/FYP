using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathQuestionManager : MonoBehaviour {
    public TextMeshProUGUI questionText;
    private int correctAnswer;
    
    void Start() {
        GenerateQuestion();
    }
    
    void GenerateQuestion() {
        int a = Random.Range(1, 5);
        int b = Random.Range(1, 5);
        correctAnswer = a + b; // 範例：加法題
        questionText.text = $"{a} + {b} = ?";
    }
    
    public bool CheckAnswer(int playerAnswer) {
        return playerAnswer == correctAnswer;
    }
}
