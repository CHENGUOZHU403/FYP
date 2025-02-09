using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public GameObject saveSlotUI; 

    public void StartGame()
    {
        saveSlotUI.SetActive(true);
    }

    public void ConfirmSaveSlot(string saveFileName)
    {
        PlayerPrefs.SetString("CurrentSave", saveFileName);
        SceneManager.LoadScene("Prologue");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}