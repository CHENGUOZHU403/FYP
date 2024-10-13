using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    //public List<QuestionAndAns> QnA;
    public int QuestionA;
    public int QuestionB;
    public int Answer;
    public int[] optionOffset = {0, 1, 2, -1};
    public int RandomNum;
    
    public List<int> optionsArr = new List<int>(){1,2,3,4};

    public GameObject[] options;
    //public int ccurrentQuestion;

    public Text QuestionText;

    // Start is called before the first frame update
    void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        //QnA.RemoveAt(ccurrentQuestion);
        generateQuestion();
    }

    void SetAnswer()
    {
        for(int i = 0; i < optionsArr.Count; i++)
        {
            optionsArr[i] = optionOffset[i] + Answer;
        }

        ShuffleList(optionsArr);

        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            
            //options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[ccurrentQuestion].Answers[i];

            options[i].transform.GetChild(0).GetComponent<Text>().text = optionsArr[i].ToString();
            
            if(optionsArr[i] == Answer)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
            //if (QnA[ccurrentQuestion].CorrectAnswer == i + 1)
            //{
            //    options[i].GetComponent<AnswerScript>().isCorrect = true;
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateQuestion()
    {
        //ccurrentQuestion = Random.Range(0, QnA.Count);
        QuestionA = Random.Range(0, 5);
        QuestionB = Random.Range(0, 5);

        //QuestionText.text = QnA[ccurrentQuestion].Question;
        QuestionText.text = QuestionA + " + " + QuestionB + " = ?";
        Answer = QuestionA + QuestionB;
        SetAnswer();
    }

    public List<int> ShuffleList(List<int> list)
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
}
