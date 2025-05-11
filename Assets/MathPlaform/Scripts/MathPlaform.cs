using UnityEngine;
using TMPro;
using System.Collections;

public class MathPlatform : MonoBehaviour 
{
    public int answer;
    public GameObject correctFX;
    [SerializeField] private TMP_Text questionText;
    
    // 新增平台销毁参数
    [Header("销毁设置")]
    [SerializeField] private float destroyDelay = 0.5f;
    [SerializeField] private ParticleSystem destroyEffect;
    

    public void SetQuestion(string question)
    {
        string[] parts = question.Split('+');
        int a = int.Parse(parts[0].Trim());
        int b = int.Parse(parts[1].Split('=')[0].Trim());
        answer = a + b;
        questionText.text = question;
    }

    public bool CheckAnswer(int playerAnswer)
    {
        bool isCorrect = (playerAnswer == answer);
        if (isCorrect)
        {
            StartCoroutine(DestroyPlatform());
        }
        return isCorrect;
    }

    private IEnumerator DestroyPlatform()
    {
        // 播放正确特效
        if (correctFX != null)
        {
            Instantiate(correctFX, transform.position, Quaternion.identity);
        }

        // 播放销毁特效
        if (destroyEffect != null)
        {
            var effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            effect.Play();
        }

        // 禁用组件防止重复触发
        GetComponent<Collider2D>().enabled = false;

        // 通知关卡管理器
        LevelManager.Instance.PlatformDestroyed();

        // 延迟销毁
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}