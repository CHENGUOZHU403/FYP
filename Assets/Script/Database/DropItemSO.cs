using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItemSO
{
    public GameObject itemPrefab;
    [Range(0, 1)] public float probability = 0.5f;
    public Vector2Int amountRange = new Vector2Int(1, 1);
}

// MonsterDropData.cs
[CreateAssetMenu(fileName = "New Monster Drop", menuName = "RPG/Monster Drop Data")]
public class MonsterDropData : ScriptableObject
{
    public string monsterID; // ����Ψһ��ʶ
    public List<DropItem> dropItems = new List<DropItem>();
}
