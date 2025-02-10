using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEditor;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text speakerNameText;
    public Button shopButton;
    public Button nextButton;
    public HeroKnight HeroKnight;

    public string speakerName = "???";
    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines;

    public float textDisplaySpeed = 0.05f;
    public int currentLineIndex = 0;
    private Coroutine displayCoroutine;
    public int changeNameIndex = -1;

    [Header("Black Mask")]
    public Image blackMask;
    public float fadeDuration = 2.0f;

    private void Start()
    {
        if (GameManager.Instance.isWatched)
        {
            dialoguePanel.SetActive(false);
            blackMask.enabled = false;
        }
        else
        {
            GameManager.Instance.isWatched = true;
            StartDialogue();
            nextButton.onClick.AddListener(DisplayNextLine);
        }
    }

    public void StartDialogue()
    {
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        if (changeNameIndex == currentLineIndex) { SetSpeakerName(speakerName); }
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
            if (changeNameIndex == currentLineIndex) { SetSpeakerName(speakerName); }
            displayCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
            StartCoroutine(FadeIn());
        }
    }

    public void EndDialogue()
    {
        shopButton.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        
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

    public void SetSentence(string[] sentences)
    {
        dialogueLines = sentences;
        StartDialogue();
    }
    
    public void SetSpeakerName(string speakerName)
    {
        speakerNameText.text = speakerName;
    }

    private IEnumerator FadeIn()
    {
        Color maskColor = blackMask.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            maskColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackMask.color = maskColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        maskColor.a = 0f;
        blackMask.color = maskColor;

        blackMask.gameObject.SetActive(false);
    }
}
