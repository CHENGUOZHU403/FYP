using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterEncounter : MonoBehaviour
{
    public MonsterData monster; // 拖入对应怪物数据的引用

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 保存怪物信息并跳转到战斗场景
            PlayerPrefs.SetString("EncounteredMonster", monster.name);
            SceneManager.LoadScene("BattleScene");
        }
    }
}
