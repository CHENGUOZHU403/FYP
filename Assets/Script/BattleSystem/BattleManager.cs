using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleManager : MonoBehaviour
{
    public MonsterData[] allMonsters; // 所有怪物数据
    public Image monsterImage;
    public TMP_Text monsterNameText;

    private MonsterData encounteredMonster;

    void Start()
    {
        string monsterName = PlayerPrefs.GetString("EncounteredMonster", "");
        encounteredMonster = System.Array.Find(allMonsters, m => m.name == monsterName);

        if (encounteredMonster != null)
        {
            monsterImage.sprite = encounteredMonster.monsterSprite;
            monsterNameText.text = encounteredMonster.monsterName;
            // 初始化怪物生命值、攻击力等战斗数据
        }
        else
        {
            Debug.LogError("未找到对应的怪物信息！");
        }
    }
}
