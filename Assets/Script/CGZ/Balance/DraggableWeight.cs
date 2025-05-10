using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWeight : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public Vector3 originalPosition;
    public int weight = 1;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (IsOverTray("LeftTray"))
        {
            transform.SetParent(GameObject.Find("LeftTray").transform);
        }
        else if (IsOverTray("RightTray"))
        {
            transform.SetParent(GameObject.Find("RightTray").transform);
        }
        else
        {
            // 沒有放入托盤，回原位
            rectTransform.anchoredPosition = originalPosition;
        }

        GameObject.Find("BalancePivot").GetComponent<BalanceController>().UpdateBalance();
    }

    private bool IsOverTray(string trayName)
    {
        RectTransform tray = GameObject.Find(trayName).GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(tray, Input.mousePosition);
    }
}
