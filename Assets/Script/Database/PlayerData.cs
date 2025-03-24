using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("PlayerInfo")]
    public string playerName;
    public int attackPower;
    public float attackRange;
    public int armor, mp, ap, time;
    

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

    [Header("Player Position")]
    public float positionX;
    public float positionY;
    public float positionZ;

    public void UpdataPlayerStat(int maxHealth, int armor, int mp,int ap,int time)
    {
        this.maxHealth = maxHealth;
        this.armor = armor;
        this.mp = mp;
        this.ap = ap;
        this.time = time;
    }

    public void SavePlayerPosition(Vector3 position)
    {
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;
    }

    public Vector3 LoadPlayerPosition()
    {
        return new Vector3(positionX, positionY, positionZ);
    }
    
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
