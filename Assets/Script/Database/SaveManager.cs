using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerData playerData;
    public static SaveManager Instance { get; private set; }
    public List<MonsterData> monsterList = new List<MonsterData>();

    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/game_save.json";
    }

     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void SaveGame()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        // save the data from player
        saveData["playerName"] = playerData.playerName;
        saveData["playerLevel"] = playerData.level;
        saveData["playerXP"] = playerData.currentXP;
        saveData["xpToNextLevel"] = playerData.xpToNextLevel;
        saveData["playerMaxHealth"] = playerData.maxHealth;
        saveData["playerCurrentHealth"] = playerData.currentHealth;
        saveData["playerAttackPower"] = playerData.attackPower;
        saveData["money"] = playerData.money;

        // save the monster data
        List<Dictionary<string, object>> monsterDataList = new List<Dictionary<string, object>>();
        foreach (MonsterData monster in monsterList)
        {
            Dictionary<string, object> monsterData = new Dictionary<string, object>
            {
                { "monsterName", monster.monsterName },
                { "level", monster.level },
                { "maxHealth", monster.maxHealth },
                { "attackPower", monster.attackPower },
                { "isDefeated", monster.isDefeated }
            };
            monsterDataList.Add(monsterData);
        }
        saveData["monsters"] = monsterDataList;

        // change to the Json doc
        string json = JsonUtility.ToJson(new Wrapper(saveData), true);
        File.WriteAllText(savePath, json);
        Debug.Log("save sucessfully：" + savePath);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
            Dictionary<string, object> saveData = wrapper.data;

            // load playerdata
            playerData.playerName = saveData["playerName"].ToString();
            playerData.level = int.Parse(saveData["playerLevel"].ToString());
            playerData.currentXP = int.Parse(saveData["playerXP"].ToString());
            playerData.xpToNextLevel = int.Parse(saveData["xpToNextLevel"].ToString());
            playerData.maxHealth = int.Parse(saveData["playerMaxHealth"].ToString());
            playerData.currentHealth = int.Parse(saveData["playerCurrentHealth"].ToString());
            playerData.attackPower = int.Parse(saveData["playerAttackPower"].ToString());
            playerData.money = int.Parse(saveData["money"].ToString());

            // load playerdata
            List<object> monsterDataList = (List<object>)saveData["monsters"];
            foreach (object obj in monsterDataList)
            {
                Dictionary<string, object> monsterData = (Dictionary<string, object>)obj;
                MonsterData monster = monsterList.Find(m => m.monsterName == monsterData["monsterName"].ToString());
                if (monster != null)
                {
                    monster.level = int.Parse(monsterData["level"].ToString());
                    monster.maxHealth = int.Parse(monsterData["maxHealth"].ToString());
                    monster.attackPower = int.Parse(monsterData["attackPower"].ToString());
                    monster.isDefeated = bool.Parse(monsterData["isDefeated"].ToString());
                }
            }

            Debug.Log("load data sucessfully");
        }
        else
        {
            Debug.Log("No data was found");
        }
    }

    // delete data
    public void ResetGame()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("data has been deleted");
        }
    }

    [System.Serializable]
    private class Wrapper
    {
        public Dictionary<string, object> data;
        public Wrapper(Dictionary<string, object> data)
        {
            this.data = data;
        }
    }
}
