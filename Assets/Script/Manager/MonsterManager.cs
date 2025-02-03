using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    void Start()
    {
        string monsterID = gameObject.name; // set object name to ID
        if (GameManager.Instance.IsMonsterDefeated(monsterID))
        {
            gameObject.SetActive(false); // hide gameobject
        }
    }
}
