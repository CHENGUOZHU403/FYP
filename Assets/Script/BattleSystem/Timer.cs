using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timerDuration = 5f;

    [Header("UI Elements")]
    public Slider timerSlider;
    public Text timerText;

    [Header("Dependencies")]
    public MCsystem mcSystem;
    public UiManager uiManager;

    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        timeRemaining = timerDuration;
        timerSlider.maxValue = timerDuration;
        timerSlider.value = timerDuration;
        UpdateTimerText();
    }

    void Update()
    {
        UpdateTimer();
    }
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerSlider.value = timeRemaining;
            UpdateTimerText();
        }
        else
        {
            TimesUp();
            enabled = false; // Disable further updates
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString(); 
    }



    private void TimesUp()
    {
        Debug.Log("Time's up!");
    }


    public void ResetTimer()
    {
        //UiManager.Attack();
        timeRemaining = timerDuration;
        timerSlider.value = timerDuration;
        UpdateTimerText();
        enabled = true; 
        mcSystem.Reset();
    }
}
