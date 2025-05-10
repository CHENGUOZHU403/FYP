using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private bool debugMode = true;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (debugMode) Debug.Log("玩家触发了死亡区域", this);
        
        try {
            PlatformGameManager.Instance.GameOver();
        }
        catch (System.NullReferenceException e) {
            Debug.LogError($"GameManager访问失败: {e.Message}\n自动创建新实例...", this);
            var manager = new GameObject("EmergencyManager").AddComponent<PlatformGameManager>();
            manager.GameOver();
        }
    }
}
