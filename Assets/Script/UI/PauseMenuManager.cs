using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private string giveUpSceneName;

    private bool isPaused = false;
    private bool isAnimating = false;  

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlMenu.SetActive(false);
        confirmPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf || controlMenu.activeSelf)
            {
                BackToPauseMenu();
            }
            else if (!isPaused)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void BackToPauseMenu()
    {
        if(settingsMenu.activeSelf)
        {
            StartCoroutine(AnimateClose(settingsMenu.transform, () => {
                settingsMenu.SetActive(false);
                pauseMenu.SetActive(true);
                StartCoroutine(AnimateOpen(pauseMenu.transform));
            }));
        }
        else if(controlMenu.activeSelf)
        {
            StartCoroutine(AnimateClose(controlMenu.transform, () => {
                controlMenu.SetActive(false);
                pauseMenu.SetActive(true);
                StartCoroutine(AnimateOpen(pauseMenu.transform));
            }));
        }
    }

    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        StartCoroutine(AnimateOpen(pauseMenu.transform));
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ClosePauseMenu()
    {
        StartCoroutine(AnimateClose(pauseMenu.transform, () => {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }));
    }

    public void ContinueGame()
    {
        ClosePauseMenu();
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        StartCoroutine(AnimateOpen(settingsMenu.transform));
    }

    public void OpenControls()
{
    pauseMenu.SetActive(false);
    controlMenu.SetActive(true);
    StartCoroutine(AnimateOpen(controlMenu.transform));
}

    public void GiveUp()
    {
        confirmPanel.SetActive(true);
        StartCoroutine(AnimateOpen(confirmPanel.transform));
    }

    public void ConfirmGiveUp()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(giveUpSceneName);
    }

    public void CancelGiveUp()
    {
        StartCoroutine(AnimateClose(confirmPanel.transform, () => {
            confirmPanel.SetActive(false);
        }));
    }


    private IEnumerator AnimateOpen(Transform panel)
    {
        if (isAnimating) yield break;

        isAnimating = true;
        Vector3 finalScale = new Vector3(2, 2, 2);

        panel.localScale = Vector3.zero;
        float timer = 0f;
        float duration = 0.2f; 
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / duration;
            panel.localScale = Vector3.Lerp(Vector3.zero, finalScale, progress);
            yield return null;
        }
        panel.localScale = finalScale;
        isAnimating = false;
    }

    private IEnumerator AnimateClose(Transform panel, System.Action onComplete)
    {
        if (isAnimating) yield break;
        isAnimating = true;

        Vector3 originalScale = panel.localScale;
        float timer = 0f;
        float duration = 0.2f;
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / duration;
            panel.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);
            yield return null;
        }
        panel.localScale = Vector3.zero;
        isAnimating = false;
        onComplete?.Invoke();
    }
}
