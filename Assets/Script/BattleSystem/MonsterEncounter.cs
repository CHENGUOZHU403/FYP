using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterEncounter : MonoBehaviour
{
    public MonsterData monster;



    void Start()
    {
        // 检查全局状态，如果怪物已经被击败，则销毁怪物对象
        if (GameSceneManager.Instance.isMonsterDefeated)
        {
            Destroy(gameObject); // 销毁怪物
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //存储玩家和怪物位置
            Vector3 playerPosition = other.transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

            Vector3 enemyPosition = transform.position;
            PlayerPrefs.SetFloat("EnemyPosX", enemyPosition.x);
            PlayerPrefs.SetFloat("EnemyPosY", enemyPosition.y);
            PlayerPrefs.SetFloat("EnemyPosZ", enemyPosition.z);


            // 保存怪物信息并跳转到战斗场景
            PlayerPrefs.SetString("EncounteredMonster", monster.name);

            Destroy(gameObject);

            gameObject.SetActive(false);

            

            SceneManager.LoadScene("NewBattleScene");
        }
    }
}
