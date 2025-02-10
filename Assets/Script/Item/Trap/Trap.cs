using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 5; 
    public float damageCooldown = 2f; 
    private float nextDamageTime = 0f; // 记录下一次允许扣血的时间

    private bool playerInTrap = false; 
    private HeroKnight currentPlayer; 

    private void Update()
    {
        if (playerInTrap && Time.time >= nextDamageTime)
        {
            if (currentPlayer != null)
            {
                currentPlayer.TakeDamage(damage);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentPlayer = other.GetComponent<HeroKnight>();
            if (currentPlayer != null)
            {
                currentPlayer.TakeDamage(damage); // 立即扣血
                nextDamageTime = Time.time + damageCooldown; // 设置下一次扣血时间
            }
            playerInTrap = true; //记录玩家在陷阱中
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrap = false; 
            currentPlayer = null; 
        }
    }
}
