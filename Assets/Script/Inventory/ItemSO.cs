using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange;

    public int amountToChangeStat;

    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttribute;

    public PlayerData playerData;  

    

    public void UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            //GameObject.Find("HealthManager").GetCompent<PlayerHealth>().ChangeHealth(amountToChangeStat);
            if (playerData != null)
            {
                Debug.Log("amountToChangeStat : " + amountToChangeStat);
                playerData.currentHealth += amountToChangeStat;
            }
            else
            {
                Debug.LogError("playerData is not assigned!");
            }

            if (playerData.currentHealth > playerData.maxHealth)
            {
                playerData.currentHealth = playerData.maxHealth;
            }
        }
        if (statToChange == StatToChange.mp)
        {
            //GameObject.Find("MPManager").GetCompent<PlayerMP>().ChangeMP(amountToChangeStat);
            //playerData.
        }
    }

    public enum StatToChange
    {
        none,
        health,
        mp,
        time
    }


    public enum AttributesToChange
    {
        none,
        
    }

}
