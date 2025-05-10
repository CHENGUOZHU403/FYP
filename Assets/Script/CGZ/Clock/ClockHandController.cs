using UnityEngine;

public class ClockHandController : MonoBehaviour
{
    public Transform centerPoint;
    private bool isDragging = false;

    public float angleOffset = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.transform == transform)
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 centerScreenPos = Camera.main.WorldToScreenPoint(centerPoint.position);
            Vector2 direction = mousePos - centerScreenPos;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f + angleOffset;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}