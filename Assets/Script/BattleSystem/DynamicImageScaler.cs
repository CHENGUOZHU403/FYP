using UnityEngine;
using UnityEngine.UI;

public class DynamicImageScaler : MonoBehaviour
{
    public Image imageContainer; // ָ����� Image ���

    public void SetSprite(Sprite sprite)
    {
        // ����ͼƬ
        imageContainer.sprite = sprite;

        // ��ȡͼƬ��ԭʼ�ߴ�
        Vector2 spriteSize = sprite.rect.size;

        // ��̬���������Ĵ�С
        RectTransform rt = imageContainer.GetComponent<RectTransform>();
        rt.sizeDelta = spriteSize;
    }
}
