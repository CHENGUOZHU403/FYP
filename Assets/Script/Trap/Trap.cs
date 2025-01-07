using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 5; // 每次扣的血量
    public float damageCooldown = 2f; // 扣血的冷却时间
    private float nextDamageTime = 0f; // 记录下一次允许扣血的时间

    private bool playerInTrap = false; // 记录玩家是否在陷阱中
    private HeroKnight currentPlayer; // 保存当前玩家对象

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
            playerInTrap = true; // 标记玩家在陷阱中
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrap = false; // 玩家离开陷阱
            currentPlayer = null; // 清空玩家引用
        }
    }
}
