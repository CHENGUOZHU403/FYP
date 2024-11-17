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
    }


public void ShowPlayerDamage()
    {
        hideDamage();
        playerdam.SetActive(true);
    }
    public void ShowEnemyDamage()
    {
        hideDamage();
        enemydam.SetActive(true);
    }

    public void hideDamage()
    {
        MultChoiUI.SetActive(false);
        playerdam.SetActive(false);
        enemydam.SetActive(false);
    }

    public void Gameover(string str)
    {
        gameoverText.text = str;
        GameoverUI.SetActive(true);
    }
}
