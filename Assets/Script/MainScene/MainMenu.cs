using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("BasicMap 2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}