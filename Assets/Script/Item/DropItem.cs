using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject Itemdrop;
    public int DropItemNum = 1; 
    public float dropRadius = 1f; 
    public int xpDropAmount = 20; 
    public GameObject xpOrbPrefab;
    public int moneyDropAmount = 10;

    public void Defeat()
    {
        // Assuming player reference is obtained
        HeroKnight player = FindObjectOfType<HeroKnight>();
        player.AddMoney(moneyDropAmount);
        Destroy(gameObject); // Destroy the enemy
    }
    private void OnDestroy()
    {
        for (int i = 0; i < DropItemNum; i++)
        {
            Vector3 dropPosition = transform.position + Random.insideUnitSphere * dropRadius;
            dropPosition.y = 0;

            Instantiate(Itemdrop, dropPosition, Quaternion.identity);
        }
        DropXPOrb();
    }

      void DropXPOrb()
    {
        Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
    }
}