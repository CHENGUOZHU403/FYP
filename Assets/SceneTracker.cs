using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public string previousSceneName = "Prologue"; 

    public void ReturnToPreviousScene()
    {
        GameSceneManager.Instance.isMonsterDefeated = true;
        SceneManager.LoadScene(previousSceneName);
    }
}
