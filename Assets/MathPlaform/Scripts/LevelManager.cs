using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [Header("场景设置")]
    [SerializeField] private string villageSceneName = "NoviceVillage";
    [SerializeField] private float sceneLoadDelay = 10f;
    
    [Header("UI设置")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
     public bool IsLevelCompleted { get; private set; }
    
    private int totalPlatforms;
    private int destroyedPlatforms;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        totalPlatforms = FindObjectsOfType<MathPlatform>().Length;
        destroyedPlatforms = 0;
        dialoguePanel.SetActive(false);
    }

    public void PlatformDestroyed()
    {
        destroyedPlatforms++;
        
        if (destroyedPlatforms >= totalPlatforms)
        {
            StartCoroutine(CompleteLevel());
        }
    }

    private IEnumerator CompleteLevel()
    {
        IsLevelCompleted = true; // 新增状态标记
        // 显示通关对话
        dialoguePanel.SetActive(true);
        dialogueText.text = "? ? ?：\n" +
            "「Hum...solved all the puzzles....\n" +
            "According to the contract, you can leave....」";
        
        // 等待并加载场景
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(villageSceneName);
    }
}