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

    [Header("Teleport Settings")]
    public string mainTownScene = "NoviceVillage";
    public Vector3 townSpawnPosition = Vector3.zero;

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

    public void TeleportToTown()
{
    // 保存当前场景到历史栈
    string currentScene = SceneManager.GetActiveScene().name;
    if (!sceneHistory.Contains(currentScene)) // 避免重复添加
    {
        sceneHistory.Push(currentScene);
    }
    
    // 保存玩家状态
    SavePlayerPosition();
    SceneManager.LoadScene(mainTownScene);
}

public void ReturnFromTown()
{
    if (sceneHistory.Count > 0)
    {
        string previousScene = sceneHistory.Pop();
        SceneManager.LoadScene(previousScene);
    }
    else
    {
        Debug.LogWarning("No scene history to return to");
        SceneManager.LoadScene(mainTownScene);
    }
}

private void SavePlayerPosition()
{
    if (player != null)
    {
        playerPosition = player.transform.position;
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.Save();
    }
}
}
