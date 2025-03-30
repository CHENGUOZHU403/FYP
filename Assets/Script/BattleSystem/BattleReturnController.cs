using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleReturnController : MonoBehaviour
{
    public void EndBattle()
    {
        PlayerPrefs.DeleteKey("EncounteredMonster");
        PlayerPrefs.DeleteKey("CurrentMonster");
        GameManager.Instance.ReturnToPreviousScene();
    }
}
