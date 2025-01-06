using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideNPCDialogue : MonoBehaviour
{

    public string NPCName = "Eryn";

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    private bool isPlayerNearby = false;

    private string[] dialogue = {
        "Hello, newcomer...",
        "Welcome to the world of mathematics!",
        "Oh, sorry I forgot to introduce myself. My name is..",
        "Eryn"
    };

    private int currentDialogueIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // 按E键与NPC互动
        {
            ShowNextDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            dialoguePanel.SetActive(true); // 显示对话框
            Debug.Log("Player is Nearby NPC");
            ShowNextDialogue(); // 显示第一个对话内容
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialoguePanel.SetActive(false); // 隐藏对话框
        }
    }

    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogue.Length)
        {
            dialogueText.text = dialogue[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            dialoguePanel.SetActive(false); // 对话结束时隐藏对话框
        }
    }
}
