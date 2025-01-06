using UnityEngine;

public class CoinBag : MonoBehaviour
{
    public int coinAmount = 5;  


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取玩家脚本并增加金钱
            HeroKnight HeroKnight = other.GetComponent<HeroKnight>();
            if (HeroKnight != null)
            {
                HeroKnight.AddMoney(coinAmount);  
                Destroy(gameObject); 
            }
        }
    }
}
