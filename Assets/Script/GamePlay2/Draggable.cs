using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField]
    public Transform originalParent; // 原始父对象
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform tempParent; // 临时父对象（Canvas）

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        tempParent = canvas.transform; // 设置临时父对象为 Canvas
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // 保存原始父对象
        transform.SetParent(tempParent); // 将按钮移到 Canvas 下，脱离布局影响
        canvasGroup.blocksRaycasts = false; // 禁用射线检测
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        Vector2 canvasScale = canvas.transform.localScale;
        rectTransform.anchoredPosition = new Vector2(localPoint.x / canvasScale.x, localPoint.y / canvasScale.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // 启用射线检测

        if (transform.parent == tempParent)
        {
            // 如果没有放到新的 DropZone，则回到原始父对象
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}