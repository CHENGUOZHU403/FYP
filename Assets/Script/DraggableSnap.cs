using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableSnap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalPosition;
    private Transform originalParent;

    public float snapDistance = 100f;
    public int weight = 1;
    public bool isMoneyBag = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ��ʮɦ^�� Canvas �W�A�קK���즫�L�����мv�T
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (TrySnapToTray("LeftTray") || TrySnapToTray("RightTray"))
            return;

        // �S���l�����\�A�^���l������M��l��m
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }

    private bool TrySnapToTray(string trayName)
    {
        RectTransform tray = GameObject.Find(trayName).GetComponent<RectTransform>();
        int currentItems = tray.childCount;

        if (currentItems >= 3)
            return false;

        float distance = Vector3.Distance(rectTransform.position, tray.position);

        if (distance <= snapDistance)
        {
            transform.SetParent(tray);

            switch (currentItems)
            {
                case 0:
                    rectTransform.position = tray.position;
                    break;
                case 1:
                    rectTransform.position = tray.position + new Vector3(30f, 0, 0);
                    break;
                case 2:
                    rectTransform.position = tray.position + new Vector3(-30f, 0, 0);
                    break;
            }

            transform.SetSiblingIndex(0);
            return true;
        }
        return false;
    }
}
