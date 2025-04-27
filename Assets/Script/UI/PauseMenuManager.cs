using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    // References to UI panels assigned via Inspector
    [SerializeField] private GameObject pauseMenu;      // The main pause menu panel
    [SerializeField] private GameObject settingsMenu;   // Settings sub-menu panel
    [SerializeField] private GameObject controlMenu;    // Controls sub-menu panel
    [SerializeField] private GameObject confirmPanel;   // Confirmation dialog panel
    [SerializeField] private string giveUpSceneName;    // Scene to load when "Yes" is clicked

    private bool isPaused = false; // Tracks whether the game is currently paused

    void Start()
    {
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        // Toggle pause when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                // If not paused, open the pause menu
                OpenPauseMenu();
            }
            else
            {
                // If already paused, resume the game
                ClosePauseMenu();
            }
        }
    }

    // Activates the pause menu and pauses the game
    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);      // Show pause menu UI
        Time.timeScale = 0f;           // Freeze game time
        isPaused = true;               // Update state
    }

    // Deactivates the pause menu and resumes the game
    private void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);     // Hide pause menu UI
        Time.timeScale = 1f;            // Resume game time
        isPaused = false;               // Update state
    }

    // Called by the "Continue" button: closes the pause menu
    public void ContinueGame()
    {
        ClosePauseMenu();
    }

    // Called by the "Settings" button: opens the settings panel
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    // Called by the "Control" button: opens the control panel
    public void OpenControls()
    {
        controlMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    // Called by the "Give Up" button: shows the confirmation dialog
    public void GiveUp()
    {
        confirmPanel.SetActive(true);
    }

    // Called by the "Yes" button on confirmation: load the specified scene
    public void ConfirmGiveUp()
    {
        Time.timeScale = 1f;                    // Ensure timeScale is reset
        SceneManager.LoadScene(giveUpSceneName); // Load the specified scene&#8203;:contentReference[oaicite:6]{index=6}
    }

    // Called by the "No" button on confirmation: hide confirmation panel
    public void CancelGiveUp()
    {
        confirmPanel.SetActive(false);
        // Pause menu remains active; timeScale stays 0
    }
}
