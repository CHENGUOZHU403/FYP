using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopNPCDialogue : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button ShopButton;
    public GameObject shopUI;
    private bool isDialogueActive = false;

    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines;

    [Header("Settings")]
    public float textDisplaySpeed = 0.05f; 

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private Coroutine displayCoroutine;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        shopUI.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextLine);
        ShopButton.onClick.AddListener(OpenShop);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    public void OpenShop()
    {
        if (dialoguePanel.activeSelf) return;
        shopUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
    }


    private void StartDialogue()
    {
        if (isDialogueActive) return; 
        if (shopUI.activeSelf) return;
        isDialogueActive = true;

        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }

        if (currentLineIndex < dialogueLines.Length)
        {
            displayCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDisplaySpeed);
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
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