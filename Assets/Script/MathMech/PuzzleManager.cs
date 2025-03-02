using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    
    [SerializeField] private int totalPuzzles = 3;
    private int solvedPuzzles;
    
    public System.Action OnAllPuzzlesSolved;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SolvePuzzle()
    {
        solvedPuzzles++;
        if (solvedPuzzles >= totalPuzzles)
        {
            OnAllPuzzlesSolved?.Invoke();
            Debug.Log("All Mechines unlocked!!");
        }
    }
}