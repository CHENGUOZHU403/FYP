using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject gameObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 檢查碰撞的物體是否標籤為 "Player"
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // 銷毀當前物件
            Debug.Log("...");
        }
    }
}
