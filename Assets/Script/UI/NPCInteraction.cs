using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public TMP_Text interactText; 
    public GameObject player;     
    public float interactionDistance = 3f; 

    private bool isPlayerInRange = false; 

    private void Start()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 檢測玩家是否在範圍內
        if (Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
        {
            if (!isPlayerInRange)
            {
                ShowInteractionHint();
                isPlayerInRange = true;
            }

            // 檢測玩家是否按下 E 鍵
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithNPC();
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                HideInteractionHint();
                isPlayerInRange = false;
            }
        }
    }

    private void ShowInteractionHint()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press E to interact";
        }
    }

    private void HideInteractionHint()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    private void InteractWithNPC()
    {
        Debug.Log("Interacting with NPC...");
        // 在此添加與 NPC 的具體互動邏輯
    }
}

