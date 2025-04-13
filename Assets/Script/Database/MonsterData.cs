using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterDropItem
{
    
    public enum ItemType { GoldBag, XPBall, Equipment, TeleportScroll }
    public ItemType itemType;

    [Range(0, 1)]
    public float dropProbability = 0.5f;

    public Vector2Int quantityRange = new Vector2Int(1, 1);

    public GameObject[] possibleEquipment; 
}


[CreateAssetMenu(fileName = "NewMonster", menuName = "Battle/Monster")]
public class MonsterData : ScriptableObject
{
    [Header("MonsterInfo")]
    public string monsterName;
    public GameObject monsterPrefab;

    [Header("XP Settings")]
    public int baseXP = 30;
    public float xpMultiplier = 1.5f;

    [Header("MonsterValue")]
    public int maxHealth;
    public int level;
    public int attackPower;
    public float attackRange;
    public bool isDefeated;

    [Header("Monster Type")]
    public bool isBoss = false; 

    [Header("����ϵͳ")]
    public List<MonsterDropItem> dropTable = new List<MonsterDropItem>
    {
        
        new MonsterDropItem
        {
            itemType = MonsterDropItem.ItemType.GoldBag,
            dropProbability = 1f, 
            quantityRange = new Vector2Int(1, 2)
        },
        new MonsterDropItem
        {
            itemType = MonsterDropItem.ItemType.XPBall,
            dropProbability = 1f,
            quantityRange = new Vector2Int(3, 5)
        }
    };
}
