using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
    public float moveSpeed = 2f;        // 移動速度
    public float moveDistance = 5f;    // 移動的距離
    public float pauseDuration = 1f;   // 停頓的時間

    private Vector3 startPosition;     // 起始位置
    private bool movingLeft = true;    // 是否正在向左移動

    void Start()
    {
        // 紀錄背景的初始位置
        startPosition = transform.position;
        // 開始移動協程
        StartCoroutine(MoveBackground());
    }

    private IEnumerator MoveBackground()
    {
        while (true)
        {
            // 計算目標位置
            Vector3 targetPosition = movingLeft
                ? startPosition + Vector3.left * moveDistance
                : startPosition + Vector3.right * moveDistance;

            // 移動到目標位置
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime
                );
                yield return null; // 等待下一幀
            }

            // 停頓
            yield return new WaitForSeconds(pauseDuration);

            // 反轉移動方向
            movingLeft = !movingLeft;
        }
    }
}
