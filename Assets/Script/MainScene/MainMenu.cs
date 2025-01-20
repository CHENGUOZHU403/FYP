using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Mix");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}