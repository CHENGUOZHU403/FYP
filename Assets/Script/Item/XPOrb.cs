using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpValue = 30;
    public MonsterData monsterData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HeroKnight player = other.GetComponent<HeroKnight>();
            if (player != null && monsterData.isDefeated)
            {
                player.playerData.GainXP(xpValue);
                Destroy(gameObject); 
            }
            Debug.Log("EXP");
        }
    }
}
