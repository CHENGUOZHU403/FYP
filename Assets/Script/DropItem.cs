using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject Itemdrop;
    public int DropItemNum = 1; 
    public float dropRadius = 1f; 
    public int xpDropAmount = 20; // exp
    public GameObject xpOrbPrefab; 

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
        // 生成經驗球
        Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
    }
}