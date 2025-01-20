using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public string previousSceneName = "Prologue"; 

    public void ReturnToPreviousScene()
    {
        SceneManager.LoadScene(previousSceneName);
    }
}
