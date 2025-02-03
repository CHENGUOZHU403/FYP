using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject MultChoiUI;
    public GameObject RoundEndUI;
    public GameObject GameoverUI;
    public GameObject DialoguePanle;
    public Text gameoverText;

    public GameObject buttonContainer;

    public void Attack()
    {
        MultChoiUI.SetActive(true);
        DialoguePanle.SetActive(false);
    }

    public void ChooseAction()
    {
        DialoguePanle.SetActive(true);
        buttonContainer.SetActive(true);
    }

    public void HideMultChoiUI()
    {
        MultChoiUI.SetActive(false);
    }

    public void ShowDamage()
    {
        DialoguePanle.SetActive(true);
    }

    public void GameOver(string str)
    {
        gameoverText.text = str;
        GameoverUI.SetActive(true);
        DialoguePanle.SetActive(false);
    }

    public void ShowDialogue()
    {
        DialoguePanle.SetActive(true);
        MultChoiUI.SetActive(false);
    }

}
