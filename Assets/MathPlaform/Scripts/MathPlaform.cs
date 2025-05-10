using UnityEngine;
using TMPro;

public class MathPlatform : MonoBehaviour 
{
    public int answer;
    public GameObject correctFX;
     [SerializeField] private TMP_Text questionText; // 改為private強制使用序列化欄位
    
    public void SetQuestion(string question)
    {
        // 解析题目格式如 "3 + 5 = ?"
        string[] parts = question.Split('+');
        int a = int.Parse(parts[0].Trim());
        int b = int.Parse(parts[1].Split('=')[0].Trim());
        answer = a + b;
        
        questionText.text = question;
    }

    public bool CheckAnswer(int playerAnswer)
    {
        bool isCorrect = (playerAnswer == answer);
        if (isCorrect && correctFX != null)
            Instantiate(correctFX, transform.position, Quaternion.identity);
        return isCorrect;
    }
}