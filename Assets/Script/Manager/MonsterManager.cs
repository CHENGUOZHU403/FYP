using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string monsterID = gameObject.name; // �Թ���������ΪΨһID
        if (GameManager.Instance.IsMonsterDefeated(monsterID))
        {
            gameObject.SetActive(false); // �����ѱ����ܵĹ���
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
