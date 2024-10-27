using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider TimerSlider;
    public Text TimerText;
    public float gameTime;
    public float sleepTime;

    public bool stopTimer;

    public BattleSystem battleSystem;
    public MCsystem MCsystem;
    public GameObject MultChoiUI;
    public GameObject RoundEndUI;

    public bool isStart =  false;
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
        if (isStart)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0 && stopTimer == false)
            {
                stopTimer = true;
                battleSystem.TurnEnd();
                RoundEndUI.SetActive(true);

                gameTime = sleepTime;
            }

            if (stopTimer == false)
            {
                TimerText.text = Mathf.RoundToInt(gameTime).ToString();
                TimerSlider.value = gameTime;
            }
            if (stopTimer == true)
            {
                MultChoiUI.SetActive(false);
                if (gameTime < 0)
                {
                    Reset();
                }
            }
        }

    }

    public void Reset()
    {
        MultChoiUI.SetActive(true);
        RoundEndUI.SetActive(false);
        gameTime = TimerSlider.maxValue;
        stopTimer = false;
        MCsystem.Reset();

    }

    public void start()
    {
        isStart = true;
    }
}
