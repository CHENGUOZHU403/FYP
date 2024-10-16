using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCsystem : MonoBehaviour
{
    public int QuestionA;
    public int QuestionB;
    public int Answer;
    public int Answered;
    public Text QuestionText;
    public Text CorrectCountText;
    public Text WrongCountText;


    public int CorrectNum = 0, WrongNum = 0;

    public int[] optionOffset = { 0, 1, 2, -1 };
    public List<int> optionsArr = new List<int>() { 1, 2, 3, 4 };
    public GameObject[] options;

    void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        CorrectCountText.text = "Correct :" + ++CorrectNum;
        Answered++;
        generateQuestion();
    }
    public void wrong()
    {
        WrongCountText.text = "Wrong :" + ++WrongNum;
        Answered++;
        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < optionsArr.Count; i++)
        {
            optionsArr[i] = optionOffset[i] + Answer;
        }

        ShuffleList(optionsArr);

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;

            options[i].transform.GetChild(0).GetComponent<Text>().text = optionsArr[i].ToString();

            if (optionsArr[i] == Answer)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        QuestionA = UnityEngine.Random.Range(0, 5);
        QuestionB = UnityEngine.Random.Range(0, 5);

        QuestionText.text = QuestionA + " + " + QuestionB + " = ?";
        Answer = QuestionA + QuestionB;
        SetAnswer();
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
        CorrectNum = 0;
        WrongNum = 0;
        Answered = 0;
        CorrectCountText.text = "Correct :" + CorrectNum;
        WrongCountText.text = "Wrong :" + WrongNum;
        generateQuestion();
    }
}
