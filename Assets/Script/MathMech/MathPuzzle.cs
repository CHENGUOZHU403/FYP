using UnityEngine;
using UnityEngine.UI;

public class MathPuzzle : MonoBehaviour
{
    [Header("Settings")]
    public int minNumber = 1;
    public int maxNumber = 10;
    
    [Header("References")]
    public GameObject obstacle;
    public PuzzleUI puzzleUI;
    
    private int correctAnswer;
    private bool isSolved;
    private bool playerInRange;

    private void Start()
    {
        GenerateProblem();
        puzzleUI.OnAnswerSubmitted += HandleAnswer;
    }

    private void GenerateProblem()
    {
        int num1 = Random.Range(minNumber, maxNumber);
        int num2 = Random.Range(minNumber, maxNumber);
        correctAnswer = num1 + num2;
        puzzleUI.SetProblemText($"{num1} + {num2} = ?");
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.X) && !isSolved)
        {
            puzzleUI.ToggleUI(true);
        }
    }

    private void HandleAnswer(int answer)
    {
        if (answer == correctAnswer)
        {
            isSolved = true;
            obstacle.SetActive(false);
            PuzzleManager.Instance.SolvePuzzle();
            puzzleUI.ToggleUI(false);
        }
        else
        {
            puzzleUI.ShowError("Wrong answer, try again");
            GenerateProblem();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            puzzleUI.TogglePrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            puzzleUI.TogglePrompt(false);
        }
    }
}