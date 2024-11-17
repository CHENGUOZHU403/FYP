using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel; 
    public Text dialogueText;        
    public Button nextButton;        

    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines;   // content

    private int currentLineIndex = 0; // 當前對話行索引
    private bool playerInRange = false; // 玩家是否在範圍內

    private void Start()
    {
        dialoguePanel.SetActive(false); // 隱藏對話框
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
