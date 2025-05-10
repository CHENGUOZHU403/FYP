using UnityEngine;

public class ClockHandUIController : MonoBehaviour
{
    public RectTransform centerPoint;
    private bool isDragging = false;
    public float angleOffset = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            RectTransform rt = GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(rt, mousePos, null))
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
            Vector2 centerScreenPos = RectTransformUtility.WorldToScreenPoint(null, centerPoint.position);
            Vector2 direction = mousePos - centerScreenPos;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f + angleOffset;
            GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
