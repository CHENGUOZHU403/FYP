using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleManager : MonoBehaviour
{
    public MonsterData[] allMonsters; // ���й�������
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
            // ��ʼ����������ֵ����������ս������
        }
        else
        {
            Debug.LogError("δ�ҵ���Ӧ�Ĺ�����Ϣ��");
        }
    }
}
