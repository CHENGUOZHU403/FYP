using UnityEngine;

public class CoinBag : MonoBehaviour
{
    public int coinAmount = 100;
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HeroKnight player = other.GetComponent<HeroKnight>();
            if (player != null)
            {
                player.AddMoney(coinAmount);
                Destroy(gameObject); 
                //AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }
            else
            {
                Debug.LogWarning("PlayerData reference is missing in CoinBag!");
            }
        }
    }
}