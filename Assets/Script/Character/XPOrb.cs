using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpValue = 20; // 經驗球提供的經驗值

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // use AddXp method
            HeroKnight player = other.GetComponent<HeroKnight>();
            if (player != null)
            {
                player.AddXP(xpValue);
                Destroy(gameObject); 
            }
            Debug.Log("EXP");
        }
    }
}
