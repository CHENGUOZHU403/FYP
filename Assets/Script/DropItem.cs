using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject itemPrefab;
    public int DropItemNum = 1; 
    public float dropRadius = 1f; 

    private void OnDestroy()
    {
        for (int i = 0; i < DropItemNum; i++)
        {
            Vector3 dropPosition = transform.position + Random.insideUnitSphere * dropRadius;
            dropPosition.y = 0;

            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
    }
}