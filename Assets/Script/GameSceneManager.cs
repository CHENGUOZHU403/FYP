using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    public bool isMonsterDefeated = false;
    public bool hasShownOpening = false;
    public GameObject player;      // 玩家对象
    public GameObject rewardPrefab; // 奖励预制体

    private void Awake()
    {
        // 确保只有一个实例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持对象在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            Vector3 playerPosition = new Vector3(
                PlayerPrefs.GetFloat("PlayerPosX"),
                PlayerPrefs.GetFloat("PlayerPosY"),
                PlayerPrefs.GetFloat("PlayerPosZ")
            );
            player.transform.position = playerPosition;
        }

        if (PlayerPrefs.HasKey("EnemyPosX"))
        {
            Vector3 enemyPosition = new Vector3(
                PlayerPrefs.GetFloat("EnemyPosX"),
                PlayerPrefs.GetFloat("EnemyPosY"),
                PlayerPrefs.GetFloat("EnemyPosZ")
            );
            Instantiate(rewardPrefab, enemyPosition, Quaternion.identity);
        }


        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.DeleteKey("EnemyPosX");
        PlayerPrefs.DeleteKey("EnemyPosY");
        PlayerPrefs.DeleteKey("EnemyPosZ");
        PlayerPrefs.DeleteKey("EncounteredMonster");
    }
}
