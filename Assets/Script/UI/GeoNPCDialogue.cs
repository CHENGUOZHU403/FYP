using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GeoNPCDialogue : MonoBehaviour
{

    public string NPCName = "Geo";

    private uint talkCount = 0;
    private bool isPlayerNearby = false;
    private bool isInteract = false;
    public DialogueManager dialogueManager;

    public GameObject interactionPrompt;

    public MonsterData monster;

    [SerializeField]
    private string[] dialogue = {
        "Wow, a new guy.",
        "Stop looking, I'm talking",
        "Don't wear tinted glasses, even though I'm a monster",
        "Let me introduce myself",
        "My name is Geo",
        "Let me teach you the basics of combat."
    };

    [SerializeField]
    private string[] dialogue2 =
    {

    };

    [SerializeField]
    private string[] dialogue3 =
    {

    };

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
                    changeNameIndex = 5;
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

        if (dialogueManager.isEnd == true && talkCount == 1)
        {
            StartBattle();
        }

        if (dialogueManager.isEnd)
        {
            isInteract = false;
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

    void StartBattle()
    {
        string monsterID = gameObject.name;
        PlayerPrefs.SetString("EncounteredMonster", monster.name);
        PlayerPrefs.SetString("CurrentMonster", monsterID);
        GameManager.Instance.playerPosition = gameObject.transform.position;
        SceneManager.LoadScene("NewBattleScene");
    }
}
