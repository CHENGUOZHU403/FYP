using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformGameManager : MonoBehaviour // 类名必须与调用处一致
{
    public static PlatformGameManager Instance; // 单例静态引用

    [Header("UI設定")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    private int score;
    private bool isGameOver;

    void Awake()
    {
        // 单例初始化
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 新增GameOver方法
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    // 其他原有方法...
}
