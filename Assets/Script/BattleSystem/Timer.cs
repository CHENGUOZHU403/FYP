using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public Text timerText;

    private float timeRemaining;
    public float timerDuration;

    public MCsystem MCsystem;
    public UiManager UiManager;

    public unit playerunit;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = playerunit.timeRemaining;
        timeRemaining = timerDuration;
        timerSlider.maxValue = timerDuration;
        timerSlider.value = timerDuration;
        UpdateTimerText();
    }


    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerSlider.value = timeRemaining;
            UpdateTimerText();
        }
        else
        {
            // Call the TimesUp function once when the timer reaches zero
            TimesUp();
            // Optionally, stop further updates
            enabled = false; // Disable the script to stop the timer
        }
    }

    void UpdateTimerText()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString(); // Display only seconds
    }

    private void TimesUp()
    {
        Debug.Log("Time's up!"); // Replace with your desired functionality
        // You can add any other behavior here, such as showing a message or triggering an event.
    }


    public void ResetTimer()
    {
        UiManager.Attack();
        timeRemaining = timerDuration;
        timerSlider.value = timerDuration;
        UpdateTimerText();
        enabled = true; // Re-enable the script if it was disabled
        MCsystem.Reset();
    }
}
