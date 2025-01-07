using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 5; // 每次扣的血量
    public float damageCooldown = 2f; // 扣血的冷却时间
    private float nextDamageTime = 0f; // 记录下一次允许扣血的时间

    private void OnTriggerStay2D(Collider2D other)
    {
        // 检查是否是玩家
        if (other.CompareTag("Player"))
        {
            // 如果当前时间大于等于下一次扣血时间
            if (Time.time >= nextDamageTime)
            {
                // 扣血逻辑
                HeroKnight HeroKnight = other.GetComponent<HeroKnight>();
                if (HeroKnight != null)
                {
                    HeroKnight.TakeDamage(damage);
                }

                // 更新下一次扣血时间
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 如果玩家离开陷阱区域，重置冷却时间
        if (other.CompareTag("Player"))
        {
            nextDamageTime = 0f;
        }
    }
}
