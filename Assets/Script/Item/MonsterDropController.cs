using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public MonsterData data;
    public GameObject xpOrbPrefab;
    [Range(0f, 1f)] public float xpDropChance = 1f;
    
    private int currentHealth;
    private bool isDead;

    void Start()
    {
        currentHealth = data.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(isDead) return;
        
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        TryDropXPOrb();
        Destroy(gameObject, 1f); // delay 1s to die
    }

    private void TryDropXPOrb()
    {
        if(Random.value <= xpDropChance)
        {
            int calculatedXP = CalculateXP();
            Vector3 dropPosition = transform.position + Vector3.up * 0.5f;
            
            GameObject orb = Instantiate(xpOrbPrefab, dropPosition, Quaternion.identity);
            orb.GetComponent<XPOrb>().Initialize(calculatedXP);
        }
    }

    private int CalculateXP()
    {
        return Mathf.RoundToInt(data.baseXP * Mathf.Pow(data.level, data.xpMultiplier));
    }
}