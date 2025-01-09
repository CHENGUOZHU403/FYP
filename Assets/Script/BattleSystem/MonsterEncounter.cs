using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterEncounter : MonoBehaviour
{
    public MonsterData monster;



    void Start()
    {
        // ���ȫ��״̬����������Ѿ������ܣ������ٹ������
        if (GameSceneManager.Instance.isMonsterDefeated)
        {
            Destroy(gameObject); // ���ٹ���
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //�洢��Һ͹���λ��
            Vector3 playerPosition = other.transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

            Vector3 enemyPosition = transform.position;
            PlayerPrefs.SetFloat("EnemyPosX", enemyPosition.x);
            PlayerPrefs.SetFloat("EnemyPosY", enemyPosition.y);
            PlayerPrefs.SetFloat("EnemyPosZ", enemyPosition.z);


            // ���������Ϣ����ת��ս������
            PlayerPrefs.SetString("EncounteredMonster", monster.name);

            Destroy(gameObject);

            gameObject.SetActive(false);

            

            SceneManager.LoadScene("NewBattleScene");
        }
    }
}
