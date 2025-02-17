using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int targetLevelIndex; 
    public Vector3 targetPosition; 

    public void OnClick()
    {
        PortalTrigger portal = FindObjectOfType<PortalTrigger>();
        if (portal != null)
        {
            portal.SetLevelIndex(targetLevelIndex);
            portal.SetSpawnPosition(targetPosition);
            portal.ExecuteTeleport();
        }
    }
}