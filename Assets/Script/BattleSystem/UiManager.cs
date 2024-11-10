using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject MultChoiUI;
    public GameObject RoundEndUI;
    public GameObject GameoverUI;


    public void TurnEnd()
    {
        MultChoiUI.SetActive(false);
        RoundEndUI.SetActive(true);
    }
    
    public void TurnStart()
    {
        MultChoiUI.SetActive(true);
        RoundEndUI.SetActive(false);
    }

    public void ShowDamage()
    {

    }

}
