using UnityEngine;

public class CloudScroller : MonoBehaviour
{
    public float scrollSpeed = 50f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, rectTransform.rect.width);
        rectTransform.localPosition = new Vector3(newPos, rectTransform.localPosition.y, 0);
    }
}
