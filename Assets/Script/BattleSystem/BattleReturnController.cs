using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleReturnController : MonoBehaviour
{
    public BattleManager battleManager;

    public void EndBattle()
    {
        if (battleManager.state == BattleState.Won) {
            GameManager.Instance.ReturnToPreviousScene();
        }
        else if(battleManager.state == BattleState.Lost)
        {
            GameManager.Instance.ReturnToMainTownScene();
        }
       
    }
}
