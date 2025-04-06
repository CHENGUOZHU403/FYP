using UnityEngine;
using UnityEngine.UI;

public class MathPuzzle : MonoBehaviour
{
    public enum Operation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    [Header("Settings")]
    public int minNumber = 1;
    public int maxNumber = 100;
    public bool allowDivision = true;
    public bool allowNegativeResults = false;
    
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
        Operation operation = (Operation)Random.Range(0, allowDivision ? 4 : 3);
        int num1, num2;
        string operatorSymbol = "";
        
        do
        {
            num1 = Random.Range(minNumber, maxNumber);
            num2 = Random.Range(minNumber, maxNumber);
            
            switch(operation)
            {
                case Operation.Addition:
                    correctAnswer = num1 + num2;
                    operatorSymbol = "+";
                    break;
                    
                case Operation.Subtraction:
                    correctAnswer = num1 - num2;
                    operatorSymbol = "-";
                    // If negative results are not allowed and the result is negative, regenerate the number
                    if (!allowNegativeResults && correctAnswer < 0)
                    {
                        int temp = num1;
                        num1 = num2;
                        num2 = temp;
                        correctAnswer = num1 - num2;
                    }
                    break;
                    
                case Operation.Multiplication:
                    correctAnswer = num1 * num2;
                    operatorSymbol = "×";
                    break;
                    
                case Operation.Division:
                    // Make sure the division problem is divisible
                    if (num2 == 0) num2 = 1;
                    int product = num1 * num2;
                    correctAnswer = num1;
                    num1 = product;
                    operatorSymbol = "÷";
                    break;
            }
        } 
        while (operation == Operation.Division && !allowDivision); // Make sure division is allowed

        puzzleUI.SetProblemText($"{num1} {operatorSymbol} {num2} = ?");
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