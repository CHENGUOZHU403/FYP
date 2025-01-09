using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideNPCDialogue : MonoBehaviour
{

    public string NPCName = "Eryn";

    public GameObject dialoguePanel;
    public TMP_Text speakerNameText;
    public TMP_Text dialogueText;
    private bool isPlayerNearby = false;

    private string[] dialogue = {
        "Hello, newcomer...",
        "Welcome to the world of mathematics!",
        "Oh, sorry I forgot to introduce myself.",
        "My name is Eryn"
    };

    private int currentDialogueIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F)) // ��E����NPC����
        {
            dialoguePanel.SetActive(true);
            ShowNextDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialoguePanel.SetActive(false);
            currentDialogueIndex = 0;
        }
    }

    void ShowNextDialogue()
    {
        
        if (currentDialogueIndex < dialogue.Length)
        {
            if (currentDialogueIndex == 3)
            {
                speakerNameText.text = NPCName;
            }
            dialogueText.text = dialogue[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            dialoguePanel.SetActive(false); // �Ի�����ʱ���ضԻ���
        }
    }
}
