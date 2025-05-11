using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlatformGameManager : MonoBehaviour
{
    [SerializeField] private static PlatformGameManager _instance;
    
    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text fadeText;  
    public TMP_Text deathText; 
    public Button retryButton;
    public Animator fadeAnimator;
    public float textDelay = 1f;
    public AudioClip gameOverSound;
    public AudioClip ButtonSound;
    private AudioSource audioSource;

    [Header("Scene Settings")]
    public string noviceVillageSceneName = "NoviceVillage"; // 确保这个变量名拼写正确
    public static PlatformGameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PlatformGameManager>();
                if (_instance == null) {
                    GameObject obj = new GameObject("PlatformGameManager");
                    _instance = obj.AddComponent<PlatformGameManager>();
                    Debug.LogWarning("Auto-create PlatformGameManager");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        // 确保只有一个实例存在
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        _instance = this;
        gameOverPanel.SetActive(false);
        DontDestroyOnLoad(gameObject); 
        retryButton.gameObject.SetActive(false);
        
        Debug.Log("GameManager instance complete", this);
    }
    public void GameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
        Debug.Log("Game Over!", this);
        Cursor.visible = true; // 显示鼠标光标
        gameOverPanel.SetActive(true);
        
        // 显示UI元素
        fadeText.gameObject.SetActive(true);
        deathText.gameObject.SetActive(true);
        
        // 播放渐变动画
        fadeAnimator.Play("FadeIn");
        
        // 延迟显示死亡文本
        StartCoroutine(ShowDeathTextAfterDelay(1f));
    }

    IEnumerator ShowDeathTextAfterDelay(float delay)
    {
        deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, 0);
        yield return new WaitForSecondsRealtime(delay); // 等待FadeIn动画完成

        // 渐变显示死亡文本
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            deathText.color = new Color(
                deathText.color.r,
                deathText.color.g,
                deathText.color.b,
                Mathf.Lerp(0, 1, elapsed/duration)
            );
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f); // 新增：死亡文字显示后额外等待2秒

        retryButton.gameObject.SetActive(true); // 现在总共延迟了3秒(1+2)才显示按钮
        audioSource.PlayOneShot(ButtonSound);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 恢复时间
        audioSource.PlayOneShot(ButtonSound);
        
        // 跳转到新手村场景
        SceneManager.LoadScene(noviceVillageSceneName);
        
        // 重置UI状态（因为DontDestroyOnLoad会保留Manager）
        fadeText.gameObject.SetActive(false);
        deathText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }
}