using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class ErynNPCDialogue : MonoBehaviour
{

    public string NPCName = "Eryn";

    private uint talkCount = 0;
    private bool isPlayerNearby = false;
    private bool isInteract = false;
    public DialogueManager dialogueManager;

    public GameObject interactionPrompt;

    public string[] dialogue;

    public string[] dialogue2;

    public string[] dialogue3;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && !isInteract) // Input F to interact with NPC
        {
            string[] dialog;
            int changeNameIndex;
            switch (talkCount)
            {
                case 0:
                    dialog = dialogue;
                    changeNameIndex = 3;
                    break;
                case 1:
                    dialog = dialogue2;
                    changeNameIndex = 0;
                    dialogueManager.shopButton.gameObject.SetActive(true);
                    break;
                case 2:
                default:
                    dialog = dialogue3;
                    changeNameIndex = 0;
                    dialogueManager.shopButton.gameObject.SetActive(true);
                    break;
            }
            dialogueManager.SetSentence(dialog);
            dialogueManager.speakerName = NPCName;
            dialogueManager.currentLineIndex = 0;
            dialogueManager.changeNameIndex = changeNameIndex;
            talkCount++;
            isInteract = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionPrompt.SetActive(true);
            Debug.Log("Player is Nearby NPC");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            isInteract = false;
            interactionPrompt.SetActive(false);
            dialogueManager.EndDialogue();
            Debug.Log("Player is not Nearby NPC");
        }
    }
}
