using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BalanceManager : MonoBehaviour
{
    public TextMeshProUGUI resultText; // 在 Canvas 上放一個 Text 顯示結果

    public void CheckBalance()
    {
        int leftWeight = CalculateWeight("LeftTray");
        int rightWeight = CalculateWeight("RightTray");

        if (leftWeight == rightWeight)
        {
            resultText.text = "balanced!";
        }
        else
        {
            resultText.text = "unbalanced!";
        }
    }

    private int CalculateWeight(string trayName)
    {
        Transform tray = GameObject.Find(trayName).transform;
        int total = 0;
        foreach (Transform item in tray)
        {
            DraggableWeight di = item.GetComponent<DraggableWeight>();
            if (di != null)
                total += di.weight;
        }
        return total;
    }
}
