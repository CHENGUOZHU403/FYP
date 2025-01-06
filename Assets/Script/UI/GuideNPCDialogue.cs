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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // ��E����NPC����
        {
            ShowNextDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            dialoguePanel.SetActive(true); // ��ʾ�Ի���
            Debug.Log("Player is Nearby NPC");
            ShowNextDialogue(); // ��ʾ��һ���Ի�����
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialoguePanel.SetActive(false); // ���ضԻ���
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
            dialoguePanel.SetActive(false); // �Ի�����ʱ���ضԻ���
        }
    }
}
