using UnityEngine;

public class MathPuzzle : MonoBehaviour
{
    [Header("References")]
    public GameObject obstacle;
    public PuzzleUI puzzleUI;
    public int puzzleDataIndex = 0;
    public bool useDifficultySystem = false;
    public int problemDifficulty = 1;

    [Header("Puzzle Settings")]
    public bool generateNewOnCorrect = true; 
    public bool keepObstacleDisabled = false; 

    private bool isSolved;
    private bool playerInRange;

    private void Start()
    {
        PuzzleManager.Instance.LoadPuzzleData(puzzleDataIndex);
        GenerateProblem();
        puzzleUI.OnAnswerSubmitted += HandleAnswer;
    }

    private void GenerateProblem()
    {
        MathPuzzleData.MathProblem problem;

        if (useDifficultySystem)
        {
            problem = PuzzleManager.Instance.GetProblemByDifficulty(problemDifficulty);
        }
        else
        {
            problem = PuzzleManager.Instance.GetRandomProblem();
        }

        puzzleUI.SetProblemText(problem.problemText);
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
        if (PuzzleManager.Instance.CheckAnswer(answer))
        {
            isSolved = true;
            obstacle.SetActive(false);

            puzzleUI.ToggleUI(false);

            PuzzleManager.Instance.SolvePuzzle();


            if (generateNewOnCorrect)
            {
                GenerateProblem();
                isSolved = false;

                if (!keepObstacleDisabled)
                {
                    obstacle.SetActive(true);
                }
            }
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
            puzzleUI.ToggleUI(false);
        }
    }
}