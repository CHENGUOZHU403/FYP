using UnityEngine;

public class CoinBag : MonoBehaviour
{
    public int coinAmount = 100;
    public PlayerData playerData; 
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerData != null)
            {
                playerData.AddMoney(coinAmount);
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }
            else
            {
                Debug.LogWarning("PlayerData reference is missing in CoinBag!");
            }
        }
    }
}