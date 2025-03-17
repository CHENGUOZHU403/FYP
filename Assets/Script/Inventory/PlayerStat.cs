using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public PlayerData playerData;
    public int health, armor, mp, ap, time;
    [SerializeField]
    private TMP_Text healthText, armorText, mpText, apText, timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
public void UpdateEquipmentStats()
    {
        playerData.UpdataPlayerStat(health, armor, mp, ap, time);
        healthText.text = health.ToString();
        armorText.text = armor.ToString();
        mpText.text = mp.ToString();
        apText.text = ap.ToString();
        timeText.text = time.ToString();
    }
}
