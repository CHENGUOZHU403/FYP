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
        // 根据偏移量生成选项数组
        for (int i = 0; i < optionOffset.Length; i++)
        {
            optionsArr[i] = answer + optionOffset[i];
        }

        // 随机打乱选项
        ShuffleList(optionsArr);

        // 将选项分配给按钮
        for (int i = 0; i < options.Length; i++)
        {
            var answerScript = options[i].GetComponent<AnswerScript>();
            Text optionText = options[i].transform.GetChild(0).GetComponent<Text>();

            optionText.text = optionsArr[i].ToString();
            answerScript.isCorrect = (optionsArr[i] == answer); // 标记正确选项
        }
    }

    public void GenerateQuestion()
    {
        questionA = UnityEngine.Random.Range(0, 5);
        questionB = UnityEngine.Random.Range(0, 5);

        questionText.text = $"{questionA} + {questionB} = ?";
        answer = questionA + questionB;
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
}
