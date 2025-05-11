using UnityEngine;

public class VerticalCameraFollow : MonoBehaviour
{
    public Transform player; // 角色的 Transform
    public float smoothSpeed = 0.125f; // 相機移動的平滑速度
    public Vector3 offset; // 相機與角色的偏移
    public float minY; // 相機的最小 Y 值（防止超出下方邊界）
    public float maxY; // 相機的最大 Y 值（防止超出上方邊界）

    void LateUpdate()
    {
        if (player != null)
        {
            // 計算目標相機位置
            Vector3 targetPosition = player.position + offset;

            // 限制相機的 Y 軸位置
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

            // 平滑移動相機
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // 更新相機位置
            transform.position = smoothedPosition;
        }
    }
}