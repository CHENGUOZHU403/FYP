using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameObject EquipmentMenu;
    private bool menuActivated;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("EqirpmentMenu") && menuActivated)
        {
            Time.timeScale = 1;
            EquipmentMenu.SetActive(false);
            menuActivated = false;
        }

        else if (Input.GetButtonDown("EqirpmentMenu") && !menuActivated)
        {
            Time.timeScale = 0;
            EquipmentMenu.SetActive(true);
            menuActivated = true;
        }

    }
}