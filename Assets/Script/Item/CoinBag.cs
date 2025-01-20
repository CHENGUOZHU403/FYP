using UnityEngine;

public class CoinBag : MonoBehaviour
{
    public int coinAmount = 5;  


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            HeroKnight HeroKnight = other.GetComponent<HeroKnight>();
            if (HeroKnight != null)
            {
                HeroKnight.AddMoney(coinAmount);  
                Destroy(gameObject); 
            }
        }
    }
}
