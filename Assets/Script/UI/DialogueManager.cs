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

    public string speakerName = "???";
    public Image speakerImage;
    public Sprite defaultSprite;
    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines;

    public float textDisplaySpeed = 0.05f;
    public int currentLineIndex = 0;
    private Coroutine displayCoroutine;
    public int changeNameIndex = -1;

    public HeroKnight heroKnight;

    public bool isEnd;



    public void StartDialogue()
    {
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        heroKnight.m_canMove = false;
        isEnd = false;
        if (changeNameIndex == currentLineIndex) { SetSpeakerName(speakerName); }
        DisplayNextLine();
    }

    public void DisplayNextLine()
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
            
        }
    }

    public void EndDialogue()
    {
        //shopButton.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        isEnd = true;
        heroKnight.m_canMove = true;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(textDisplaySpeed);
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
}
