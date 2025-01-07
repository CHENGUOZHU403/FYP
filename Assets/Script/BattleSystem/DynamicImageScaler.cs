using UnityEngine;
using UnityEngine.UI;

public class DynamicImageScaler : MonoBehaviour
{
    public Image imageContainer; // 指向你的 Image 组件

    public void SetSprite(Sprite sprite)
    {
        // 设置图片
        imageContainer.sprite = sprite;

        // 获取图片的原始尺寸
        Vector2 spriteSize = sprite.rect.size;

        // 动态调整容器的大小
        RectTransform rt = imageContainer.GetComponent<RectTransform>();
        rt.sizeDelta = spriteSize;
    }
}
