using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string monsterID = gameObject.name; // 以怪物名字作为唯一ID
        if (GameManager.Instance.IsMonsterDefeated(monsterID))
        {
            gameObject.SetActive(false); // 隐藏已被击败的怪物
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
