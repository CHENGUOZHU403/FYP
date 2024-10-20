using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform Maincharacter;
    public float characterhealth;
    public float Maxhealth = 100f;
    public Slider Healthbar;
    // Start is called before the first frame update
    void Start()
    {
        characterhealth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Healthbar != null)
        {
            Healthbar.enabled = true;
        }
        if (Healthbar = null) 
        {
            Healthbar.enabled = false;
        }
    }
}
