using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialoguePanel : MonoBehaviour
{
    public Button shopButton;
    public Button continueButton;
    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        shopButton.gameObject.SetActive(false);
        continueButton.onClick.AddListener(continueTalk);
    }

    private void continueTalk()
    {
        dialogueManager.DisplayNextLine();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
