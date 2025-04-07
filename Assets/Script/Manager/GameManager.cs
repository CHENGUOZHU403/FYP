using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 playerPosition;
    public GameObject player;
    private Dictionary<string, bool> defeatedMonsters = new Dictionary<string, bool>();
    private Dictionary<string, bool> generatedLootMonsters = new Dictionary<string, bool>();
    public bool isWatched = false;

    private static bool isFirst = true;

    private Stack<string> sceneHistory = new Stack<string>();

    private void Awake()
    {

        if (isFirst)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerPosition = player.GetComponent<Transform>().position;
            player.GetComponent<HeroKnight>().playerData.Reset();
            Debug.Log("playerData Reset");
            isFirst = false;
        }

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
        SceneManager.LoadScene("BattleScene");
    }

    public void ReturnToPreviousScene()
    {
        string previousScene = sceneHistory.Pop();
        string defeatedMonsterID = PlayerPrefs.GetString("CurrentMonster");
        MarkMonsterAsDefeated(defeatedMonsterID);
        SceneManager.LoadScene(previousScene);

    }

    public void MarkMonsterAsDefeated(string enemyID)
    {
        if (defeatedMonsters.ContainsKey(enemyID))
        {
            defeatedMonsters[enemyID] = true;
        }
        else
        {
            defeatedMonsters.Add(enemyID, true);
        }
    }

    public void MarkMonsterAsGeneratedLoot(string enemyID)
    {
        if (generatedLootMonsters.ContainsKey(enemyID))
        {
            generatedLootMonsters[enemyID] = true;
        }
        else
        {
            generatedLootMonsters.Add(enemyID, true);
        }
    }


    public bool IsMonsterDefeated(string enemyID)
    {
        return defeatedMonsters.ContainsKey(enemyID) && defeatedMonsters[enemyID];
    }

    public bool IsMonsterGeneratedLoot(string enemyID)
    {
        return generatedLootMonsters.ContainsKey(enemyID) && generatedLootMonsters[enemyID];
    }
}
