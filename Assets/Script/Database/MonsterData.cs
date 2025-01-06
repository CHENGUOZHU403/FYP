using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Battle/Monster")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public Sprite monsterSprite;
    public int maxHealth;
    public int attackPower;
}
