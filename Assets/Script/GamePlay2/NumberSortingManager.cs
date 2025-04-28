using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberSortingManager : MonoBehaviour
{
    public GameObject numberPrefab;
    public GameObject dropZonePrefab;
    public Transform panel;
    public int numberCount = 5;
    public bool isSorted = false;

    [SerializeField]
    private List<int> numbers = new List<int>();
    [SerializeField]
    private List<GameObject> numberButtons = new List<GameObject>();
    [SerializeField]
    private List<GameObject> dropZones = new List<GameObject>();

    public TextMeshProUGUI alartText;


    void Start()
    {
        GenerateNumbers();
        CreateNumberButtons();

        alartText.text = "";
        isSorted = false;
    }

    // Randomly generate a set of numbers
    void GenerateNumbers()
    {
        numbers.Clear();
        for (int i = 1; i <= numberCount; i++)
        {
            numbers.Add(i);
        }

        // Shuffle the numbers
        for (int i = 0; i < numbers.Count; i++)
        {
            int temp = numbers[i];
            int randomIndex = Random.Range(0, numbers.Count);
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }
    }

    // Create new NumberButtons
    void CreateNumberButtons()
    {
        if (numberPrefab == null || dropZonePrefab == null || panel == null)
        {
            Debug.LogError("NumberPrefab, DropZonePrefab, or Panel is not assigned in the Inspector!");
            return;
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            // Create DropZone 
            GameObject dropZone = Instantiate(dropZonePrefab, panel);
            dropZones.Add(dropZone);

            // Put the button into DropZone
            GameObject button = Instantiate(numberPrefab, dropZone.transform);
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = numbers[i].ToString();

            numberButtons.Add(button);
        }
    }

    // Check the order is Correct
    public void CheckSort()
    {
        
        for (int i = 0; i < numberButtons.Count; i++)
        {
            int number = int.Parse(dropZones[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
            if (number != i + 1)
            {
                isSorted = false;
                break;
            }
            isSorted = true;
        }

        if (isSorted)
        {
            Debug.Log("Sorted correctly");
            alartText.text = "Sorted correctly";

        }
        else
        {
            Debug.Log("Sorted incorrectly, Try Again");
            alartText.text = "Sorted incorrectly, Try Again";
        }
    }
}
