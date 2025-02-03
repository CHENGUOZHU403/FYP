using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Battle/Monster")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int level;
    public Sprite monsterSprite;
    public int maxHealth;
    public int attackPower;
    public bool isDefeated;
    public GameObject monsterPrefab;
    public Animator animator;
}
