using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterEncounter : MonoBehaviour
{
    public MonsterData monster;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string monsterID = gameObject.name;
            PlayerPrefs.SetString("EncounteredMonster", monster.name);
            PlayerPrefs.SetString("CurrentMonster", monsterID);
            GameManager.Instance.playerPosition = other.transform.position;
            SceneManager.LoadScene("NewBattleScene");
        }
    }
}
