using UnityEngine;

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
}
