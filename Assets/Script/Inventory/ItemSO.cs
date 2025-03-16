using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange= new StatToChange();
    public int amountToChangeStat;

    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttribute;

    public void UseItem()
    {
        if (statToChange == StatToChange.health)
        {
          //GameObject.Find("HealthManager").GetCompent<PlayerHealth>().ChangeHealth(amountToChangeStat);
        }
        if (statToChange == StatToChange.mp)
        {
            //GameObject.Find("MPManager").GetCompent<PlayerMP>().ChangeMP(amountToChangeStat);
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
