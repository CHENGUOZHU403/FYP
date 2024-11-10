using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    public void SetHUD(unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.text = hpSlider.value + " / " + hpSlider.maxValue;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpText.text = hpSlider.value + " / " + hpSlider.maxValue;
    }
}
