using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour

{
    public string scene; 

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchSceneOnClick);
    }

    public void SwitchSceneOnClick()
    {
        
        SceneManager.LoadScene(scene);
    }
}
