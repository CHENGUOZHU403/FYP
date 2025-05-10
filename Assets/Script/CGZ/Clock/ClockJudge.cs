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
        // �ץ��ɰw�M���w������
        float hourAngle = (hourHand.eulerAngles.z - hourHandOffset + 360) % 360;
        float minuteAngle = (minuteHand.eulerAngles.z - minuteHandOffset + 360) % 360;

        // �p��ɶ�
        int currentHour = Mathf.RoundToInt((360 - hourAngle) / 30) % 12;
        int currentMinute = Mathf.RoundToInt((360 - minuteAngle) / 6) % 60;

        // �����令�H 5 �����
        currentMinute = Mathf.RoundToInt(currentMinute / 5f) * 5;

        Debug.Log($"�ثe�ɶ��G{currentHour}�I {currentMinute}��");

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
