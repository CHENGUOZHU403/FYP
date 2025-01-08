using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 5; 
    public float damageCooldown = 2f; 
    private float nextDamageTime = 0f; // ��¼��һ�������Ѫ��ʱ��

    private bool playerInTrap = false; 
    private HeroKnight currentPlayer; 

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
            playerInTrap = true; //��¼�����������
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrap = false; 
            currentPlayer = null; 
        }
    }
}
