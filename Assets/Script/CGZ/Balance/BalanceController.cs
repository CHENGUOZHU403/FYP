using UnityEngine;
using TMPro;

public class BalanceController : MonoBehaviour
{
    public Transform leftTray;
    public Transform rightTray;

    public TextMeshProUGUI resultText;

    public float maxAngle = 15f;      
    public float rotateSpeed = 5f;    

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

        if(leftWeight == rightWeight)
        {
            if (CheckPuzzleCondition())
                resultText.text = "Correct!";
            else
                resultText.text = "Balanced, but incorrect...";
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
        // 特定謎題條件示範：左邊放錢袋，右邊總重量等於7

        bool leftHasMoneyBag = false;
        int rightTotalWeight = 0;

        foreach (Transform item in leftTray)
        {
            var draggable = item.GetComponent<DraggableSnap>();
            if (draggable != null && draggable.isMoneyBag)
                leftHasMoneyBag = true;
        }

        foreach (Transform item in rightTray)
        {
            var draggable = item.GetComponent<DraggableSnap>();
            if (draggable != null)
                rightTotalWeight += draggable.weight;
        }

        return leftHasMoneyBag && rightTotalWeight == 7;
    }
}
