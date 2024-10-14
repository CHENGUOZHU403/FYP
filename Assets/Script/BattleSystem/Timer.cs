using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider TimerSlider;
    public Text TimerText;
    public float gameTime;

    public bool stopTimer;
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        TimerSlider.maxValue = gameTime;
        TimerSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            stopTimer = true;
            
        }

        if(stopTimer == false) {
            TimerText.text = Mathf.RoundToInt(gameTime).ToString();
            TimerSlider.value = gameTime;
        }
    }

    public void Reset()
    {
        gameTime = 10;
        stopTimer = false;
        
    }
}
