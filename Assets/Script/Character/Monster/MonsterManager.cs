using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    public MonsterData monsterData;
    string monsterID;
    [Header("DropIitemPrefabs")]
    public GameObject goldBagPrefab;
    public GameObject xpBallPrefab;
    public GameObject teleportScrollPrefab; 
    [Range(0f, 1f)] public float teleportDropChance = 0.3f;

    public PortalController portalAnimtor;
    public PortalController portalAnimtor2;

    BoxCollider2D boxCollider2d;


    private void Awake()
    {
        monsterID = gameObject.name;
        Debug.Log(monsterID);
        boxCollider2d = GetComponent<BoxCollider2D>();
        portalAnimtor = GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalController>();
        portalAnimtor2 = GameObject.FindGameObjectWithTag("Portal2").GetComponent<PortalController>();
    }

    private void OnEnable()
    {
        if (GameManager.Instance.IsMonsterDefeated(monsterID))
        {
            boxCollider2d.enabled = false;
            if (!GameManager.Instance.IsMonsterGeneratedLoot(monsterID))
            {
                GenerateLoot(transform.position);
                GameManager.Instance.MarkMonsterAsGeneratedLoot(monsterID);
            }
            if (monsterData.isBoss)
            {
                Debug.Log("i am BOSS");
                portalAnimtor.OpenPortal();
                portalAnimtor2.OpenPortal();
            }
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("EncounteredMonster", monsterData.name);
            PlayerPrefs.SetString("CurrentMonster", monsterID);
            GameManager.Instance.playerPosition = other.transform.position;
            GameManager.Instance.EnterBattle();
        }
    }

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

            if (monsterData.isBoss && Random.value <= teleportDropChance)
            {
                 SpawnTeleportScroll(spawnPosition);
            }

            SpawnItems(itemToSpawn, spawnPosition, quantity);
        }
    }

    private GameObject GetPrefabByType(MonsterDropItem.ItemType type, MonsterDropItem drop)
    {
        switch (type)
        {
            case MonsterDropItem.ItemType.TeleportScroll: 
                return teleportScrollPrefab;
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
            Vector2 offset = Random.insideUnitCircle * 0.5f; // Spawn offset
            GameObject item = Instantiate(
                prefab,
                position + offset,
                Quaternion.identity
            );

            if (item.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.AddForce(new Vector2(
                    Random.Range(-1f, 1f),
                    Random.Range(2f, 4f)
                ), ForceMode2D.Impulse);
            }
        }
    }

    private void SpawnTeleportScroll(Vector2 position)
    {
        Vector2 offset = Random.insideUnitCircle * 0.5f;
        Instantiate(teleportScrollPrefab, position + offset, Quaternion.identity);
    }
}