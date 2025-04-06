using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    public MonsterData monsterData;
    [Header("基础掉落物预制体")]
    public GameObject goldBagPrefab;
    public GameObject xpBallPrefab;

    public void GenerateLoot(Vector2 spawnPosition)
    {
        foreach (MonsterDropItem drop in monsterData.dropTable)
        {
            if (Random.value > drop.dropProbability) continue;

            GameObject itemToSpawn = GetPrefabByType(drop.itemType, drop);
            if (itemToSpawn == null) continue;

            int quantity = Random.Range(
                drop.quantityRange.x,
                drop.quantityRange.y + 1
            );

            SpawnItems(itemToSpawn, spawnPosition, quantity);
        }
    }

    private GameObject GetPrefabByType(MonsterDropItem.ItemType type, MonsterDropItem drop)
    {
        switch (type)
        {
            case MonsterDropItem.ItemType.GoldBag:
                return goldBagPrefab;
            case MonsterDropItem.ItemType.XPBall:
                return xpBallPrefab;
            case MonsterDropItem.ItemType.Equipment:
                if (drop.possibleEquipment.Length == 0) return null;
                return drop.possibleEquipment[Random.Range(0, drop.possibleEquipment.Length)];
            default:
                return null;
        }
    }

    private void SpawnItems(GameObject prefab, Vector2 position, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 1.5f; // 分散掉落位置
            GameObject item = Instantiate(
                prefab,
                position + offset,
                Quaternion.identity
            );

            // 添加物理效果（可选）
            if (item.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.AddForce(new Vector2(
                    Random.Range(-2f, 2f),
                    Random.Range(3f, 5f)
                ), ForceMode2D.Impulse);
            }
        }
    }

    void Start()
    {
        string monsterID = gameObject.name; // set object name to ID
        if (GameManager.Instance.IsMonsterDefeated(monsterID))
        {
            GenerateLoot(transform.position);
            gameObject.SetActive(false); // hide gameobject
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string monsterID = gameObject.name;
            PlayerPrefs.SetString("EncounteredMonster", monsterData.name);
            PlayerPrefs.SetString("CurrentMonster", monsterID);
            GameManager.Instance.playerPosition = other.transform.position;
            GameManager.Instance.EnterBattle();
        }
    }
}