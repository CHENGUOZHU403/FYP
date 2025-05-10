using UnityEngine;
using TMPro;

public class BalanceController : MonoBehaviour
{
    public Transform leftTray;
    public Transform rightTray;

    public TextMeshProUGUI resultText;

    public float maxAngle = 15f;      
    public float rotateSpeed = 5f;

    public bool isCorrect;

    void Update()
    {
        UpdateBalance();
    }

    public void UpdateBalance()
    {
        int leftWeight = CalculateWeight(leftTray);
        int rightWeight = CalculateWeight(rightTray);

        int weightDiff = rightWeight - leftWeight;

        float targetAngle = Mathf.Clamp(-weightDiff * 5f, -maxAngle, maxAngle);

        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * rotateSpeed);
        transform.eulerAngles = new Vector3(0, 0, currentAngle);

        if (leftWeight == rightWeight)
        {
            if (CheckPuzzleCondition())
            {
                resultText.text = "Correct!";
                isCorrect = true;
            }
            else
            {
                resultText.text = "Balanced, but incorrect...";
                isCorrect = false;
            }
                
        }
        else
        {
            resultText.text = "Unbalanced";
            isCorrect = false;
        }
    }

    private int CalculateWeight(Transform tray)
    {
        int total = 0;
        foreach (Transform item in tray)
        {
            var draggable = item.GetComponent<DraggableSnap>();
            if (draggable != null)
                total += draggable.weight;
        }
        return total;
    }

    private bool CheckPuzzleCondition()
    {
        bool leftHasMoneyBag = false;
        bool rightHasMoneyBag = false;
        int leftTotalWeight = 0;
        int rightTotalWeight = 0;

        foreach (Transform item in leftTray)
        {
            var draggable = item.GetComponent<DraggableSnap>();
            if (draggable != null)
            {
                if (draggable.isMoneyBag)
                    leftHasMoneyBag = true;
                else
                    leftTotalWeight += draggable.weight;
            }
        }

        foreach (Transform item in rightTray)
        {
            var draggable = item.GetComponent<DraggableSnap>();
            if (draggable != null)
            {
                if (draggable.isMoneyBag)
                    rightHasMoneyBag = true;
                else
                    rightTotalWeight += draggable.weight;
            }
        }

        bool leftCorrect = leftHasMoneyBag && rightTotalWeight == 7;
        bool rightCorrect = rightHasMoneyBag && leftTotalWeight == 7;

        return leftCorrect || rightCorrect;
    }

}
