using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int health, armor, mp, ap, time;
    

    public void EquipItem()
    {
        PlayerStat playerstats = GameObject.Find("StatManager").GetComponent<PlayerStat>();
        playerstats.health += health;
        playerstats.armor += armor;
        playerstats.mp += mp;
        playerstats.ap += ap;
        playerstats.time += time;

        playerstats.UpdateEquipmentStats();
    }
    public void UnEquipItem()
    {
        PlayerStat playerstats = GameObject.Find("StatManager").GetComponent<PlayerStat>();
        playerstats.health -= health;
        playerstats.armor -= armor;
        playerstats.mp -= mp;
        playerstats.ap -= ap;
        playerstats.time -= time;

        playerstats.UpdateEquipmentStats();
    }
}
