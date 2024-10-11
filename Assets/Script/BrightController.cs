//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Rendering.PostProcessing;
//
//public class BrightnessController : MonoBehaviour
//{
//    public Slider brightnessSlider;
//
//    public PostProcessProfile brightness;
//    public PostProcessLayer layer;
//
//    Autoexposure exposure;
//    // Start is called before the first frame update
//    void Start()
//    {
//        brightness.tryGetSettings(out exposure);
//        AdjustBrightness(brightnessSlider.value);
//    }
//
//    // Update is called once per frame
//    void AdjustBrightness(float value)
//    {
//        if(value != 0)
//        {
//            exposure.keyValue.value = value;
//        }
//        else
//        {
//            exposure.keyValue.value = .05f;
//        }
//    }
//}