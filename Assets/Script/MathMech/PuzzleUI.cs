using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject puzzlePanel;
    public Text problemText;
    public InputField answerInput;
    public Text promptText;
    public Text errorText;

    public System.Action<int> OnAnswerSubmitted;

    private void Start()
    {
        ToggleUI(false);
        TogglePrompt(false);
    }

    public void ToggleUI(bool show)
    {
        puzzlePanel.SetActive(show);
        answerInput.text = "";
        errorText.gameObject.SetActive(false);
        
        if (show)
        {
            answerInput.ActivateInputField();
            EventSystem.current.SetSelectedGameObject(answerInput.gameObject);
        }
    }

    public void TogglePrompt(bool show)
    {
        promptText.gameObject.SetActive(show);
    }

    public void SetProblemText(string text)
    {
        problemText.text = text;
    }

    public void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }

    public void SubmitAnswer()
    {
        if (int.TryParse(answerInput.text, out int answer))
        {
            OnAnswerSubmitted?.Invoke(answer);
        }
        else
        {
            ShowError("Please input the right answer");
        }
    }
}