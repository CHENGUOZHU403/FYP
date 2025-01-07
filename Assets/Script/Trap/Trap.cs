using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 5; // ÿ�ο۵�Ѫ��
    public float damageCooldown = 2f; // ��Ѫ����ȴʱ��
    private float nextDamageTime = 0f; // ��¼��һ�������Ѫ��ʱ��

    private bool playerInTrap = false; // ��¼����Ƿ���������
    private HeroKnight currentPlayer; // ���浱ǰ��Ҷ���

    private void Update()
    {
        if (playerInTrap && Time.time >= nextDamageTime)
        {
            if (currentPlayer != null)
            {
                currentPlayer.TakeDamage(damage);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentPlayer = other.GetComponent<HeroKnight>();
            if (currentPlayer != null)
            {
                currentPlayer.TakeDamage(damage); // ������Ѫ
                nextDamageTime = Time.time + damageCooldown; // ������һ�ο�Ѫʱ��
            }
            playerInTrap = true; // ��������������
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrap = false; // ����뿪����
            currentPlayer = null; // ����������
        }
    }
}
