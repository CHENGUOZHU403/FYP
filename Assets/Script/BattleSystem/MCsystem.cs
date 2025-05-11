using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCsystem : MonoBehaviour
{
    [Header("Question Settings")]
    public int questionA;
    public int questionB;
    public int answer;
    public Text questionText;

    [Header("Options Settings")]
    public int[] optionOffset = { 0, 1, 2, -1 };
    public List<int> optionsArr = new List<int>() { 1, 2, 3, 4 };
    public GameObject[] options;

    [Header("Statistics")]
    public int correctCount = 0;
    public int wrongCount = 0;    
    public int answeredCount = 0; 
    public float accuracy = 0;    


    [Header("UI Elements")]
    public Text correctCountText;
    public Text wrongCountText;
    public Text accuracyText;


    public string currentMonsterType;

    void Start()
    {
        GenerateQuestion();
    }

    public void Correct()
    {
        correctCount++;
        UpdateStatistics();
        GenerateQuestion();
    }
    public void Wrong()
    {
        wrongCount++;
        UpdateStatistics();
        GenerateQuestion();
    }

    private void UpdateStatistics()
    {
        answeredCount++;
        accuracy = (float)correctCount / answeredCount * 100;

        correctCountText.text = $"Correct: {correctCount}";
        wrongCountText.text = $"Wrong: {wrongCount}";
        accuracyText.text = $"Accuracy: {Mathf.RoundToInt(accuracy)}%";
    }

    private void SetAnswerOptions()
    {
        for (int i = 0; i < optionOffset.Length; i++)
        {
            optionsArr[i] = answer + optionOffset[i];
        }

        ShuffleList(optionsArr);

        for (int i = 0; i < options.Length; i++)
        {
            var answerScript = options[i].GetComponent<AnswerScript>();
            Text optionText = options[i].transform.GetChild(0).GetComponent<Text>();

            optionText.text = optionsArr[i].ToString();
            answerScript.isCorrect = (optionsArr[i] == answer);
        }
    }

    public void GenerateQuestion()
    {
        switch (currentMonsterType)
        {
            case "MutantRat":
                questionA = UnityEngine.Random.Range(0, 10);
                questionB = UnityEngine.Random.Range(0, 10);
                questionText.text = $"{questionA} + {questionB} = ?";
                answer = questionA + questionB;
                break;

            case "Undead":
                questionA = UnityEngine.Random.Range(1, 5);
                questionB = UnityEngine.Random.Range(1, 5);
                questionText.text = $"{questionA} ¡Ñ {questionB} = ?";
                answer = questionA * questionB;
                break;

            case "Viper":
                questionA = UnityEngine.Random.Range(5, 15);
                questionB = UnityEngine.Random.Range(0, questionA);
                questionText.text = $"{questionA} - {questionB} = ?";
                answer = questionA - questionB;
                break;

            case "Beholder":
                int op1 = UnityEngine.Random.Range(0, 10);
                int op2 = UnityEngine.Random.Range(0, 10);
                int op3 = UnityEngine.Random.Range(0, 10);

                string[] operatorsPool = { "+", "-", "*", "+", "-" };
                string opSymbol1 = operatorsPool[UnityEngine.Random.Range(0, operatorsPool.Length)];

                string[] nextOperators = (opSymbol1 == "*") ? new string[] { "+", "-" } : operatorsPool;
                string opSymbol2 = nextOperators[UnityEngine.Random.Range(0, nextOperators.Length)];

                string expression = $"{op1} {opSymbol1} {op2} {opSymbol2} {op3}";
                answer = EvaluateExpression(op1, opSymbol1, op2, opSymbol2, op3);

                questionText.text = $"{expression} = ?";
                break;

            default: // Àq»{ÃD«¬
                questionA = UnityEngine.Random.Range(0, 5);
                questionB = UnityEngine.Random.Range(0, 5);
                questionText.text = $"{questionA} + {questionB} = ?";
                answer = questionA + questionB;
                break;
        }

        SetAnswerOptions();
    }


    List<int> ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    public void Reset()
    {
        correctCount = 0;
        wrongCount = 0;
        answeredCount = 0;
        accuracy = 0;

        correctCountText.text = $"Correct: {correctCount}";
        wrongCountText.text = $"Wrong: {wrongCount}";
        accuracyText.text = $"Accuracy: {Mathf.RoundToInt(accuracy)}%";

        GenerateQuestion();
    }

    private int EvaluateExpression(int a, string op1, int b, string op2, int c)
    {
        if (op2 == "*")
        {
            int second = ApplyOperator(b, op2, c);
            return ApplyOperator(a, op1, second);
        }
        else
        {
            int firstCalc = ApplyOperator(a, op1, b);
            return ApplyOperator(firstCalc, op2, c);
        }
    }

    private int ApplyOperator(int left, string op, int right)
    {
        switch (op)
        {
            case "+": return left + right;
            case "-": return left - right;
            case "*": return left * right;
            default: return 0;
        }
    }
}
