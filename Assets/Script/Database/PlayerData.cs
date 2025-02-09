using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("PlayerInfo")]
    public string playerName;
    public int attackPower;
    public float attackRange;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("Leveling")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    public GameObject playerPrefab;

    [Header("Currency")]
    public int money = 0;


    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if(amount > money)
        {
            return false;
        }
        else
        {
            money -= amount;
            return true;
        }
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        maxHealth += 5; 
        currentHealth = maxHealth;
        xpToNextLevel += 50; 
    }

    public void Reset()
    {
        maxHealth = 100;
        currentHealth = 100;
        level = 1;
        currentXP = 0;
        xpToNextLevel = 100;
        money = 0;
    }
}
