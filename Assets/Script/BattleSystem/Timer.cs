using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider TimerSlider;
    public Text TimerText;
    public float gameTime;

    public BattleSystem battleSystem;
    public MCsystem MCsystem;

    public UiManager UiManager;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 5;
    }

    private void Update()
    {
        gameTime -= Time.deltaTime;
        TimerSlider.value = gameTime;
        TimerText.text = Mathf.Round(gameTime).ToString();
        if (gameTime <= 0)
        {
            UiManager.TurnEnd();
        }
    }

    public void Reset()
    {
        UiManager.TurnStart();
        gameTime = TimerSlider.maxValue;
        MCsystem.Reset();
    }
}
