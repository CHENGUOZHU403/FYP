using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterEncounter : MonoBehaviour
{
    public MonsterData monster; // �����Ӧ�������ݵ�����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ���������Ϣ����ת��ս������
            PlayerPrefs.SetString("EncounteredMonster", monster.name);
            SceneManager.LoadScene("BattleScene");
        }
    }
}
