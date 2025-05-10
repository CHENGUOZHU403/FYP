using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class ReturnToMainMenu : MonoBehaviour
{
    // Function to load the main menu scene
    public void ReturnToMainMenuScene()
    {
        // Replace "MainMenu" with the exact name of your Main Menu scene
        SceneManager.LoadScene("MainScene");
    }
}