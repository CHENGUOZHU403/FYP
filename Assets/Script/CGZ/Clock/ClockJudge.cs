using UnityEngine;
using TMPro;

public class ClockJudge : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public int targetHour = 3;
    public int targetMinute = 15;

    public float hourHandOffset = -50f;  
    public float minuteHandOffset = 45f;

    public TextMeshProUGUI textMeshPro;

    public void CheckTime()
    {
        // 修正時針和分針的角度
        float hourAngle = (hourHand.eulerAngles.z - hourHandOffset + 360) % 360;
        float minuteAngle = (minuteHand.eulerAngles.z - minuteHandOffset + 360) % 360;

        // 計算時間
        int currentHour = Mathf.RoundToInt((360 - hourAngle) / 30) % 12;
        int currentMinute = Mathf.RoundToInt((360 - minuteAngle) / 6) % 60;

        // 分鐘改成以 5 為單位
        currentMinute = Mathf.RoundToInt(currentMinute / 5f) * 5;

        Debug.Log($"目前時間：{currentHour}點 {currentMinute}分");

        if (currentHour == targetHour && currentMinute == targetMinute)
        {
            Debug.Log("Correct!");
            textMeshPro.text = "Correct!";
        }
        else
        {
            Debug.Log("Incorrect!");
            textMeshPro.text = "Incorrect!";
        }
    }
}
