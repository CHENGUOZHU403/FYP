using System.Collections.Generic;
using UnityEngine;

public class TrayManager : MonoBehaviour
{
    public Vector3[] snapOffsets = new Vector3[]
    {
        new Vector3(0, 0, 0),     // ¤¤¶¡
        new Vector3(-30, 0, 0),   // ¥ª
        new Vector3(30, 0, 0)     // ¥k
    };

    [SerializeField]
    private List<int> occupiedSlots = new List<int>();

    public bool TryAssignSlot(DraggableSnap item, out Vector3 targetPosition)
    {
        for (int i = 0; i < snapOffsets.Length; i++)
        {
            if (!occupiedSlots.Contains(i))
            {
                occupiedSlots.Add(i);
                item.assignedSlotIndex = i;
                targetPosition = transform.position + snapOffsets[i];
                return true;
            }
        }
        targetPosition = Vector3.zero;
        return false;
    }

    public void ReleaseSlot(DraggableSnap item)
    {
        if (item.assignedSlotIndex != -1)
        {
            occupiedSlots.Remove(item.assignedSlotIndex);
            item.assignedSlotIndex = -1;
        }
    }
}
