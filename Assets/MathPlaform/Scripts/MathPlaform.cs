//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class MathPlatform : MonoBehaviour 
//{
//    public int answer; // 此平台代表的正确答案
//    public GameObject fxCorrect; // 正确反馈特效（需在Unity中拖入预制体）
//
//    // 新增方法：检查玩家答案
//    public bool CheckAnswer(int playerAnswer)
//    {
//        bool isCorrect = (playerAnswer == answer);
//        
//        if (isCorrect && fxCorrect != null)
//        {
//            Instantiate(fxCorrect, transform.position, Quaternion.identity);
//        }
//        else if (!isCorrect)
//        {
//            StartCoroutine(DisappearTemporarily());
//        }
//        
//        return isCorrect;
//    }
//
//    IEnumerator DisappearTemporarily()
//    {
//        GetComponent<Collider2D>().enabled = false;
//        GetComponent<SpriteRenderer>().color = Color.red;
//        yield return new WaitForSeconds(1.5f);
//        GetComponent<Collider2D>().enabled = true;
//        GetComponent<SpriteRenderer>().color = Color.white;
//    }
//    public string GetQuestion() 
//{
//    // 示例：生成加法题（实际逻辑可按需修改）
//    int a = Random.Range(1, 5);
//    int b = Random.Range(1, 5);
//    answer = a + b; // 存储正确答案
//    return $"{a} + {b} = ?";
//}
//}
using UnityEngine;

public class MathPlatform : MonoBehaviour 
{
    public int answer;
    public GameObject correctFX;
    
    public string GetQuestion()
    {
        // 示例：生成10以内的加法题
        int a = Random.Range(1, 5);
        int b = Random.Range(1, 5);
        answer = a + b; // 存储正确答案
        return $"{a} + {b} = ?";
    }

    public bool CheckAnswer(int playerAnswer)
    {
        bool isCorrect = (playerAnswer == answer);
        if (isCorrect && correctFX != null)
            Instantiate(correctFX, transform.position, Quaternion.identity);
        return isCorrect;
    }
}
