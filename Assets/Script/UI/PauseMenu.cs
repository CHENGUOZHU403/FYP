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
    public GameObject secmenu;
    public string scene; 
    //private AudioSource audio;
    //public AudioClip sound;

    void Start()
    {
        pausemenu.SetActive(false);

        ReToGameButton.onClick.AddListener(OnReToGameClick);
        SettingsButton.onClick.AddListener(OnSettingsClick);
        BackButton.onClick.AddListener(OnBackClick);
        //audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            secmenu.SetActive(false);
            //audio.PlayOneShot(sound);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
           //if (other.gameObject.CompareTag("Player") && !player.Isfindsister)
           //{
           //audio.PlayOneShot(sound);
           //}
        }
    }

    private void TogglePauseMenu()
    {
        pausemenu.SetActive(!pausemenu.activeSelf);
        Time.timeScale = pausemenu.activeSelf ? 0 : 1;
    }

    void OnReToGameClick()
    {
        Debug.Log("Return to game");
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }

    void OnSettingsClick()
    {
    }

    void OnBackClick()
    {
        Debug.Log("Back to main menu");
        SceneManager.LoadScene(scene);
    }
}