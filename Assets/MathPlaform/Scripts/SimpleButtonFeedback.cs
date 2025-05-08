using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; // 需导入DOTween插件
using UnityEngine.UI;

public class SimpleButtonFeedback : MonoBehaviour, IPointerDownHandler
{
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(0.9f, 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => transform.DOScale(1f, 0.4f));
        
        GetComponent<Image>().DOColor(Color.gray, 0.1f)
            .OnComplete(() => GetComponent<Image>().DOColor(Color.white, 0.3f));
    }
}