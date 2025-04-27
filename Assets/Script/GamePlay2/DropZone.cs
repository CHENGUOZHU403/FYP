using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Button currentButton; // ��ǰ�����ڵİ�ť

    void Start()
    {
        currentButton = GetComponentInChildren<Button>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Button droppedObject = eventData.pointerDrag.GetComponent<Button>(); // ��ȡ���϶��Ķ���
        if (droppedObject != null)
        {
            Draggable draggable = droppedObject.GetComponent<Draggable>();
            if (draggable != null)
            {
                // ��ȡԭʼ���ӣ���ťԭʼ������� DropZone �ű���
                DropZone originalDropZone = draggable.originalParent.GetComponent<DropZone>();

                // �����ǰ�����Ѿ��а�ť���򽻻�λ��
                if (currentButton != null)
                {
                    // ����ǰ��ť�ƶ���ԭʼ����
                    currentButton.transform.SetParent(draggable.originalParent);
                    currentButton.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    // ����ԭʼ���ӵ� currentButton
                    if (originalDropZone != null)
                    {
                        originalDropZone.currentButton = currentButton;
                    }
                }
                else
                {
                    // �����ǰ����û�а�ť�����ԭʼ���ӵ� currentButton
                    if (originalDropZone != null)
                    {
                        originalDropZone.currentButton = null;
                    }
                }

                // ����ק�İ�ť���뵱ǰ����
                droppedObject.transform.SetParent(transform);
                droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // ���µ�ǰ���ӵ� currentButton
                currentButton = droppedObject;

                // ������ק��ť��ԭʼ������
                draggable.originalParent = transform;
            }
        }
        else
        {
            Debug.LogWarning("Dropped object is null!");
        }
    }
}
