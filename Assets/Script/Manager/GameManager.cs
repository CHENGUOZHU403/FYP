using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 playerPosition;
    private Dictionary<string, bool> defeatedMonsters = new Dictionary<string, bool>();
    public bool isWatched = false;

    private Stack<string> sceneHistory = new Stack<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Let GameManager switch scene would not destory
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnterBattle()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        sceneHistory.Push(currentScene);
        SceneManager.LoadScene("NewBattleScene");
    }

    public void ReturnToPreviousScene()
    {
            string previousScene = sceneHistory.Pop();
            SceneManager.LoadScene(previousScene);

    }

    public void MarkMonsterAsDefeated(string enemyID)
    {
        if (!defeatedMonsters.ContainsKey(enemyID))
        {
            defeatedMonsters[enemyID] = true;
        }
    }

    public bool IsMonsterDefeated(string enemyID)
    {
        return defeatedMonsters.ContainsKey(enemyID) && defeatedMonsters[enemyID];
    }
}
