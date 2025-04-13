using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeleportScroll : MonoBehaviour
{
    [Header("UI References")]
    public GameObject teleportPromptUI;
    public TMP_Text promptText;
    
    [Header("Settings")]
    public KeyCode teleportKey = KeyCode.B;
    public float floatHeight = 0.3f;
    public float floatSpeed = 2f;
    
    private bool isCollected = false;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        if (teleportPromptUI != null) teleportPromptUI.SetActive(false);
        StartCoroutine(FloatAnimation());
    }

    private IEnumerator FloatAnimation()
    {
        while (true)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }

    private void Update()
    {
        if (isCollected && Input.GetKeyDown(teleportKey))
        {
            GameManager.Instance.TeleportToTown();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            CollectScroll();
        }
    }

    private void CollectScroll()
    {
        isCollected = true;
        
        // 隐藏精灵但保持脚本运行
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        // 显示UI提示
        if (teleportPromptUI != null)
        {
            teleportPromptUI.SetActive(true);
            promptText.text = $"Press {teleportKey} to return to Town";
        }
        
        // 播放拾取音效/特效
        if (TryGetComponent(out AudioSource audio))
        {
            audio.Play();
        }
    }
}