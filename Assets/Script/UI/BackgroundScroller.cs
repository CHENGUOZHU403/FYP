using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // 滾動速度
    public float backgroundWidth = 10f; // 背景圖片的寬度
    private Transform[] backgrounds; // 背景物件陣列

    void Start()
    {
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i); // 獲取背景子物件
        }
    }

    void Update()
{
    // 控制背景滾動
    for (int i = 0; i < backgrounds.Length; i++)
    {
        backgrounds[i].Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        
        // 當背景移出畫面時，重設位置
        if (backgrounds[i].position.x <= -backgroundWidth)
        {
            Vector3 newPosition = backgrounds[i].position;
            newPosition.x += backgroundWidth * backgrounds.Length; // 移到最右側
            backgrounds[i].position = newPosition;
        }
    }

    // 更新 UI 按鈕位置
    foreach (Transform uiElement in transform) // 確保 uiElement 是 BackgroundManager 的子物件
    {
        if (uiElement.CompareTag("UI")) // 假設UI元素有特定的Tag
        {
            uiElement.localPosition = new Vector3(backgrounds[0].localPosition.x, backgrounds[0].localPosition.y, 0); // 調整為相對位置
        }
    }
}
}