using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 5; // ÿ�ο۵�Ѫ��
    public float damageCooldown = 2f; // ��Ѫ����ȴʱ��
    private float nextDamageTime = 0f; // ��¼��һ�������Ѫ��ʱ��

    private void OnTriggerStay2D(Collider2D other)
    {
        // ����Ƿ������
        if (other.CompareTag("Player"))
        {
            // �����ǰʱ����ڵ�����һ�ο�Ѫʱ��
            if (Time.time >= nextDamageTime)
            {
                // ��Ѫ�߼�
                HeroKnight HeroKnight = other.GetComponent<HeroKnight>();
                if (HeroKnight != null)
                {
                    HeroKnight.TakeDamage(damage);
                }

                // ������һ�ο�Ѫʱ��
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �������뿪��������������ȴʱ��
        if (other.CompareTag("Player"))
        {
            nextDamageTime = 0f;
        }
    }
}
