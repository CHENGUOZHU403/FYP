using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleReturnController : MonoBehaviour
{
    public void EndBattle()
    {
        GameManager.Instance.ReturnToPreviousScene();
    }
}
