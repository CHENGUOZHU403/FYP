using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("NoviceVillage");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}