using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    [Header("Puzzle Data Settings")]
    [SerializeField] private MathPuzzleData[] allPuzzleDatas;

    [Header("Boss Progression Settings")]
    [SerializeField] private int totalPuzzlesRequired = 3;
    private int solvedPuzzlesCount;

    private MathPuzzleData currentPuzzleData;
    private MathPuzzleData.MathProblem currentProblem;

    public System.Action OnAllPuzzlesSolved;

    public bool AllPuzzlesSolved => solvedPuzzlesCount >= totalPuzzlesRequired;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Puzzle Data Management
    public void LoadPuzzleData(int dataIndex)
    {
        if (dataIndex >= 0 && dataIndex < allPuzzleDatas.Length)
        {
            currentPuzzleData = allPuzzleDatas[dataIndex];
        }
    }

    public MathPuzzleData.MathProblem GetRandomProblem()
    {
        if (currentPuzzleData != null)
        {
            currentProblem = currentPuzzleData.GetRandomProblem();
            return currentProblem;
        }
        return new MathPuzzleData.MathProblem();
    }

    public MathPuzzleData.MathProblem GetProblemByDifficulty(int difficulty)
    {
        if (currentPuzzleData != null)
        {
            currentProblem = currentPuzzleData.GetProblemByDifficulty(difficulty);
            return currentProblem;
        }
        return new MathPuzzleData.MathProblem();
    }

    public bool CheckAnswer(int playerAnswer)
    {
        return currentProblem.correctAnswer == playerAnswer;
    }
    #endregion

    #region Boss Puzzle Progression
    public void SolvePuzzle()
    {
        solvedPuzzlesCount++;
        Debug.Log($"Puzzles solved: {solvedPuzzlesCount}/{totalPuzzlesRequired}");

        if (AllPuzzlesSolved)
        {
            OnAllPuzzlesSolved?.Invoke();
            Debug.Log("All Machines unlocked! Boss fight available!");
        }
    }

    public void ResetPuzzleProgress()
    {
        solvedPuzzlesCount = 0;
    }
    #endregion
}