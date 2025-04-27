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
    
    [SerializeField]
    private List<int> numbers = new List<int>();
    [SerializeField]
    private List<GameObject> numberButtons = new List<GameObject>();
    [SerializeField]
    private List<GameObject> dropZones = new List<GameObject>();


    void Start()
    {
        GenerateNumbers();
        CreateNumberButtons();
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
            // 创建 DropZone 容器
            GameObject dropZone = Instantiate(dropZonePrefab, panel);
            dropZones.Add(dropZone);

            // 创建按钮并设置到 DropZone 下
            GameObject button = Instantiate(numberPrefab, dropZone.transform);
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = numbers[i].ToString();

            numberButtons.Add(button);
        }
    }

    // Check the order is Correct
    public bool CheckSort()
    {
        bool isSorted = true;
        for (int i = 0; i < numberButtons.Count; i++)
        {
            int number = int.Parse(dropZones[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
            Debug.Log(number);
            if (number != i + 1)
            {
                isSorted = false;
                break;
            }
        }

        if (isSorted)
        {
            Debug.Log("排序正确！");
        }
        else
        {
            Debug.Log("排序错误，请继续！");
        }
        return isSorted;
    }
}
