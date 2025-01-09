using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class NormalDialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel; // 對話框面板
    public TMP_Text dialogueText; // 對話文本
    public Button yesButton; // 是按鈕
    public Button noButton; // 否按鈕

    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines; // 對話內容

    private bool isPlayerInTrigger = false; // 標記玩家是否在觸發區域
    private int currentLineIndex = 0; // 當前對話行索引
    private Coroutine displayCoroutine = null; // 顯示對話的協程

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
            StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 確保觸發對象是玩家
        {
            isPlayerInTrigger = true; // 標記玩家進入觸發區域
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // 標記玩家離開觸發區域
            dialoguePanel.SetActive(false); // 離開時隱藏對話框
        }
    }

    private void StartDialogue()
    {
        currentLineIndex = 0; // 重置索引
        dialoguePanel.SetActive(true); // 顯示對話框
        DisplayNextLine(); // 開始顯示第一句對話
    }

    private void DisplayNextLine()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine); // 停止之前的顯示協程
        }

        if (currentLineIndex < dialogueLines.Length)
        {
            displayCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex])); // 顯示當前對話
            currentLineIndex++; // 移動到下一行
        }
        else
        {
            EndDialogue(); // 完成對話
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // 清空文本
        foreach (char letter in sentence)
        {
            dialogueText.text += letter; // 按字符顯示對話
            yield return new WaitForSeconds(0.05f); // 等待一段時間後顯示下一字符
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false); // 隱藏對話框
    }

    private void OnYesButtonClicked()
    {
        // 轉換場景到指定的場景（請根據需要更改場景名稱）
        SceneManager.LoadScene("Prologue");
    }

    private void OnNoButtonClicked()
    {
        dialoguePanel.SetActive(false); // 隱藏對話框
    }
}