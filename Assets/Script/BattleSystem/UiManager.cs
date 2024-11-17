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
    public GameObject playerdam;
    public GameObject enemydam;
    public Text gameoverText;

    public void Attack()
    {
        MultChoiUI.SetActive(true);
        DialoguePanle.SetActive(false);
    }

    public void ChooseAction()
    {
        DialoguePanle.SetActive(true);
        enemydam.SetActive(false);
    }

    public void ShowPlayerDamage()
    {
        MultChoiUI.SetActive(false);
        enemydam.SetActive(false);
        playerdam.SetActive(true);
    }

    public void ShowEnemyDamage()
    {
        playerdam.SetActive(false);
        enemydam.SetActive(true);
    }

    public void Gameover(string str)
    {
        gameoverText.text = str;
        GameoverUI.SetActive(true);
    }

    public void ShowDialogue()
    {
        DialoguePanle.SetActive(true);
        MultChoiUI.SetActive(false);
    }

}
