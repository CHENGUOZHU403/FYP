using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public GameObject BattleScene;
    public GameObject InGameScene;
    public BattleSystem BattleSystem;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            BattleScene.SetActive(true);
            InGameScene.SetActive(false);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleSystem.MonsterDead == true)
        {
            Destroy(gameObject);
        }
    }
}
