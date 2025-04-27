using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField]
    public Transform originalParent; // ԭʼ������
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform tempParent; // ��ʱ������Canvas��

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        tempParent = canvas.transform; // ������ʱ������Ϊ Canvas
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // ����ԭʼ������
        transform.SetParent(tempParent); // ����ť�Ƶ� Canvas �£����벼��Ӱ��
        canvasGroup.blocksRaycasts = false; // �������߼��
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
        canvasGroup.blocksRaycasts = true; // �������߼��

        if (transform.parent == tempParent)
        {
            // ���û�зŵ��µ� DropZone����ص�ԭʼ������
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}