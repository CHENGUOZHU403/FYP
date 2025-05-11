using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableSnap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalPosition;
    private Transform originalParent;
    [SerializeField]
    private TrayManager trayManager;

    public float snapDistance = 100f;
    public int weight = 1;
    public bool isMoneyBag = false;
    public int assignedSlotIndex = -1;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        if (trayManager != null)
        {
            trayManager.ReleaseSlot(this);
            trayManager = null;
        }
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
        {
            trayManager = transform.parent.GetComponent<TrayManager>();
            return;
        }

        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }
    private bool TrySnapToTray(string trayName)
    {
        TrayManager tray = GameObject.Find(trayName).GetComponent<TrayManager>();
        float distance = Vector3.Distance(rectTransform.position, tray.transform.position);

        if (distance <= snapDistance)
        {
            transform.SetParent(tray.transform);

            Vector3 snapPos;
            if (tray.TryAssignSlot(this, out snapPos))
            {
                rectTransform.position = snapPos;
                transform.SetSiblingIndex(0);
                return true;
            }
        }
        return false;
    }
}
