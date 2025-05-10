using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClockChest : MonoBehaviour
{
    public GameObject clockPanel;
    public GameObject moneyBag;

    public ClockJudge clockJudge;

    public string chestID;

    void Start()
    {
        if (GameManager.Instance.IsChestOpened(chestID))
        {
            gameObject.SetActive(false); // 已經開過了，不再顯示
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                clockPanel.SetActive(true);
            }
        }
    }

    public void CheckCorrect()
    {
        if (clockJudge.isCorrect == true)
        {
            clockPanel.SetActive(false);
            SpawnItems(moneyBag, gameObject.transform.position, 3);
            GameManager.Instance.OpenChest(chestID);
            Destroy(gameObject);
        }
    }


    private void SpawnItems(GameObject prefab, Vector2 position, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 0.5f; // Spawn offset
            GameObject item = Instantiate(
                prefab,
                position + offset,
                Quaternion.identity
            );

            if (item.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.AddForce(new Vector2(
                    Random.Range(-1f, 1f),
                    Random.Range(-4f, -6f)
                ), ForceMode2D.Impulse);
            }
        }
    }
}
