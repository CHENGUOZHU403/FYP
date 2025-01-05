using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    [Header("Brightness Settings")]
    public Slider brightnessSlider;    // 亮度滑塊
    public TMP_Text brightnessValueText; // 顯示亮度數值的文本
    public Image screenOverlay;        // 用於模擬亮度效果的覆蓋層

    [Header("Sound Settings")]
    public Slider soundSlider;         // 聲音滑塊
    public TMP_Text soundValueText;    // 顯示聲音數值的文本

    private void Start()
    {
        // 初始化滑塊值
        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
        soundSlider.onValueChanged.AddListener(UpdateSound);

        // 設置初始值
        brightnessSlider.value = 0.5f; // 假設默認亮度為50%
        soundSlider.value = AudioListener.volume; // 音量使用 AudioListener
    }

    private void UpdateBrightness(float value)
    {
        // 更新亮度數值顯示
        brightnessValueText.text = Mathf.RoundToInt(value * 100) + "%";

        // 使用 Image 的透明度模擬亮度
        if (screenOverlay != null)
        {
            Color overlayColor = screenOverlay.color;
            overlayColor.a = 1 - value; // 亮度越高，覆蓋層越透明
            screenOverlay.color = overlayColor;
        }
    }

    private void UpdateSound(float value)
    {
        // 更新聲音數值顯示
        soundValueText.text = Mathf.RoundToInt(value * 100) + "%";

        // 使用 AudioListener 控制全局音量
        AudioListener.volume = value;
    }
}

