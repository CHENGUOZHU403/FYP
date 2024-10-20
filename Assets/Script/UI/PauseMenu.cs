using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public Button ReToGameButton;
    public Button SettingsButton;
    public Button BackButton;
    public string scene; 

    void Start()
    {
        pausemenu.SetActive(false);

        ReToGameButton.onClick.AddListener(OnReToGameClick);
        SettingsButton.onClick.AddListener(OnSettingsClick);
        BackButton.onClick.AddListener(OnBackClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        pausemenu.SetActive(!pausemenu.activeSelf);
        Time.timeScale = pausemenu.activeSelf ? 0 : 1;
    }

    void OnReToGameClick()
    {
        Debug.Log("Return");
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }

    void OnSettingsClick()
    {
    }

    void OnBackClick()
    {
        Debug.Log("Back");
        SceneManager.LoadScene(scene);
    }
}