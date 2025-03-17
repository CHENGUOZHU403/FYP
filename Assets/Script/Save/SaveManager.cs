// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Linq;

// public class SaveManager : MonoBehaviour
// {
//     public PlayerData playerData;
//     public GameObject playerPrefab; 
//     private GameObject player;
    
//     public static SaveManager Instance { get; private set; }
//     public List<MonsterData> monsterList = new List<MonsterData>();

//     private string savePath;

//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }

//         savePath = Application.persistentDataPath + "/game_save.json";
//         SceneManager.sceneLoaded += OnSceneLoaded; 
//     }

//     void OnDestroy()
//     {
//         SceneManager.sceneLoaded -= OnSceneLoaded; 
//     }

//     void Start()
//     {
//         FindPlayer();
//     }

//     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         FindPlayer();
//         if (player == null && playerPrefab != null)
//         {
//             SpawnPlayer(); 
//         }
//     }

//     void FindPlayer()
//     {
//         if (player == null)
//         {
//             player = GameObject.FindGameObjectWithTag("Player");
//             if (player != null)
//             {
//                 Debug.Log(" player found：" + player.name);
//                 player.transform.position = playerData.LoadPlayerPosition(); 
//             }
//             else
//             {
//                 Debug.Log(" cant find player");
//             }
//         }
//     }

//     void SpawnPlayer()
//     {
//         Vector3 spawnPosition = playerData.LoadPlayerPosition();
//         player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
//         Debug.Log("spawned player：" + spawnPosition);
//     }

//     public void SaveGame()
//     {
//         if (player != null)
//         {
//             playerData.SavePlayerPosition(player.transform.position);
//         }

//         Dictionary<string, object> saveData = new Dictionary<string, object>
//         {
//             { "playerName", playerData.playerName },
//             { "playerLevel", playerData.level },
//             { "playerXP", playerData.currentXP },
//             { "xpToNextLevel", playerData.xpToNextLevel },
//             { "playerMaxHealth", playerData.maxHealth },
//             { "playerCurrentHealth", playerData.currentHealth },
//             { "playerAttackPower", playerData.attackPower },
//             { "money", playerData.money },
//             { "positionX", playerData.positionX },
//             { "positionY", playerData.positionY },
//             { "positionZ", playerData.positionZ }
//         };

//         List<Dictionary<string, object>> monsterDataList = new List<Dictionary<string, object>>();
//         foreach (MonsterData monster in monsterList)
//         {
//             Dictionary<string, object> monsterData = new Dictionary<string, object>
//             {
//                 { "monsterName", monster.monsterName },
//                 { "level", monster.level },
//                 { "maxHealth", monster.maxHealth },
//                 { "attackPower", monster.attackPower },
//                 { "isDefeated", monster.isDefeated }
//             };
//             monsterDataList.Add(monsterData);
//         }
//         saveData["monsters"] = monsterDataList;

//         string json = JsonUtility.ToJson(new Wrapper(saveData), true);
//         File.WriteAllText(savePath, json);
//         Debug.Log(" save sucessfully：" + savePath);
//     }

//     public void LoadGame()
//     {
//         if (File.Exists(savePath))
//         {
//             string json = File.ReadAllText(savePath);
//             Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
//             Dictionary<string, object> saveData = wrapper.data;

//             playerData.playerName = saveData["playerName"].ToString();
//             playerData.level = int.Parse(saveData["playerLevel"].ToString());
//             playerData.currentXP = int.Parse(saveData["playerXP"].ToString());
//             playerData.xpToNextLevel = int.Parse(saveData["xpToNextLevel"].ToString());
//             playerData.maxHealth = int.Parse(saveData["playerMaxHealth"].ToString());
//             playerData.currentHealth = int.Parse(saveData["playerCurrentHealth"].ToString());
//             playerData.attackPower = int.Parse(saveData["playerAttackPower"].ToString());
//             playerData.money = int.Parse(saveData["money"].ToString());
            
//             playerData.positionX = float.Parse(saveData["positionX"].ToString());
//             playerData.positionY = float.Parse(saveData["positionY"].ToString());
//             playerData.positionZ = float.Parse(saveData["positionZ"].ToString());

//             List<object> monsterDataList = (List<object>)saveData["monsters"];
//             foreach (object obj in monsterDataList)
//             {
//                 Dictionary<string, object> monsterData = (Dictionary<string, object>)obj;
//                 MonsterData monster = monsterList.Find(m => m.monsterName == monsterData["monsterName"].ToString());
//                 if (monster != null)
//                 {
//                     monster.level = int.Parse(monsterData["level"].ToString());
//                     monster.maxHealth = int.Parse(monsterData["maxHealth"].ToString());
//                     monster.attackPower = int.Parse(monsterData["attackPower"].ToString());
//                     monster.isDefeated = bool.Parse(monsterData["isDefeated"].ToString());
//                 }
//             }

//             Debug.Log(" load data sucessfully");
//         }
//         else
//         {
//             Debug.Log(" no data can beload");
//         }
//     }

//     public void ResetGame()
//     {
//         if (File.Exists(savePath))
//         {
//             File.Delete(savePath);
//             Debug.Log("save has been deleted");
//         }
//     }

//     [System.Serializable]
//     private class Wrapper
//     {
//         public Dictionary<string, object> data;
//         public Wrapper(Dictionary<string, object> data)
//         {
//             this.data = data;
//         }
//     }
// }
