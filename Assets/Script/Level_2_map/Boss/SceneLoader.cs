using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Settings")]
    public string bossSceneName = "Level_1_BossRoom";
    public float transitionDelay = 1f;
    public Vector3 transferPoint;
    private bool canTransition;

    public void InitiateBossTransition()
    {
        if (canTransition)
        {
            //GameManager.Instance.SetPlayerControl(false);
            Invoke(nameof(LoadBossScene), transitionDelay);
        }
    }

    private void LoadBossScene()
    {
        GameManager.Instance.playerPosition = transferPoint;
        SceneManager.LoadScene(bossSceneName, LoadSceneMode.Single);
    }

    public void EnableTransition() => canTransition = true;
}