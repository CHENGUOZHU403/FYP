using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalDialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel; // 設置對話框面板
    public Text dialogueText; // 設置對話文本
    public Button yesButton; // 是按鈕
    public Button noButton; // 否按鈕

    private bool isPlayerInTrigger = false; // 標記玩家是否在觸發區域

    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines;

    [Header("Settings")]
    public float textDisplaySpeed = 0.05f; 

    private void Start()
    {
        // 隱藏對話框
        dialoguePanel.SetActive(false);

        // 添加按鈕事件
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    private void Update()
    {
        // 如果玩家在觸發區域並按下 "F" 鍵，顯示對話框
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            ShowDialogue("Do you want to change the scene?");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 確保觸發對象是玩家
        {
            isPlayerInTrigger = true; // 標記玩家進入觸發區域
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // 標記玩家離開觸發區域
            dialoguePanel.SetActive(false); // 隱藏對話框
        }
    }

    private void ShowDialogue(string dialogue)
    {
        dialogueText.text = dialogue; // 顯示對話內容
        dialoguePanel.SetActive(true); // 顯示對話框
    }

    private void OnYesButtonClicked()
    {
        // 轉換場景到第一個場景（請根據需要更改場景名稱）
        SceneManager.LoadScene("SceneName1");
    }

    private void OnNoButtonClicked()
    {
        dialoguePanel.SetActive(false); // 隱藏對話框
    }
}