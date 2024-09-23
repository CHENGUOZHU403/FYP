using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
   
    public void GoToScene(string sceneName)
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(sceneName);
        
    }
    

}
