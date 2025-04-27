using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Button currentButton; // 当前格子内的按钮

    void Start()
    {
        currentButton = GetComponentInChildren<Button>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Button droppedObject = eventData.pointerDrag.GetComponent<Button>(); // 获取被拖动的对象
        if (droppedObject != null)
        {
            Draggable draggable = droppedObject.GetComponent<Draggable>();
            if (draggable != null)
            {
                // 获取原始格子（按钮原始父对象的 DropZone 脚本）
                DropZone originalDropZone = draggable.originalParent.GetComponent<DropZone>();

                // 如果当前格子已经有按钮，则交换位置
                if (currentButton != null)
                {
                    // 将当前按钮移动到原始格子
                    currentButton.transform.SetParent(draggable.originalParent);
                    currentButton.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    // 更新原始格子的 currentButton
                    if (originalDropZone != null)
                    {
                        originalDropZone.currentButton = currentButton;
                    }
                }
                else
                {
                    // 如果当前格子没有按钮，清空原始格子的 currentButton
                    if (originalDropZone != null)
                    {
                        originalDropZone.currentButton = null;
                    }
                }

                // 将拖拽的按钮放入当前格子
                droppedObject.transform.SetParent(transform);
                droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // 更新当前格子的 currentButton
                currentButton = droppedObject;

                // 更新拖拽按钮的原始父对象
                draggable.originalParent = transform;
            }
        }
        else
        {
            Debug.LogWarning("Dropped object is null!");
        }
    }
}
