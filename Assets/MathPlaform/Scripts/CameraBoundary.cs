using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    [Header("Border settings")]
    public Vector2 minBoundary; // 左下角世界坐标
    public Vector2 maxBoundary; // 右上角世界坐标
    
    private Camera cam;
    private float halfCamWidth, halfCamHeight;

    void Start()
    {
        cam = GetComponent<Camera>();
        CalculateCameraSize();

        Collider2D levelBounds = GameObject.FindWithTag("LevelBounds").GetComponent<Collider2D>();
        if (levelBounds != null)
        {
            minBoundary = levelBounds.bounds.min;
            maxBoundary = levelBounds.bounds.max;
            CalculateCameraSize();
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        
        // 计算摄像机允许移动的范围
        float clampedX = Mathf.Clamp(pos.x, minBoundary.x + halfCamWidth, maxBoundary.x - halfCamWidth);
        float clampedY = Mathf.Clamp(pos.y, minBoundary.y + halfCamHeight, maxBoundary.y - halfCamHeight);
        
        transform.position = new Vector3(clampedX, clampedY, pos.z);
    }

    void CalculateCameraSize()
    {
        halfCamHeight = cam.orthographicSize;
        halfCamWidth = halfCamHeight * cam.aspect;
    }

    // 在Scene视图显示边界（调试用）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3(
            (minBoundary.x + maxBoundary.x) / 2,
            (minBoundary.y + maxBoundary.y) / 2,
            0
        );
        Vector3 size = new Vector3(
            maxBoundary.x - minBoundary.x,
            maxBoundary.y - minBoundary.y,
            1
        );
        Gizmos.DrawWireCube(center, size);
    }
}