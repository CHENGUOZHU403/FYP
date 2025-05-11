using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClockChest : MonoBehaviour
{
    public GameObject clockPanel;
    public GameObject moneyBag;
    public GameObject[] equipmentPrefabs;

    public ClockJudge clockJudge;

    public string chestID;
    public GameObject prompt;

    void Start()
    {
        prompt = transform.GetChild(0).gameObject;
        if (prompt != null)
        {
            prompt.SetActive(false);
        }
        if (GameManager.Instance.IsChestOpened(chestID))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                clockPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.SetActive(false);
        }
    }

    public void CheckCorrect()
    {
        if (clockJudge.isCorrect == true)
        {
            clockPanel.SetActive(false);
            int moneyCount = Random.Range(1, 4);
            SpawnItems(moneyBag, transform.position, moneyCount);
            GameObject equipmentPrefab = GetEquipmentPrefab();
            SpawnItems(equipmentPrefab, transform.position, 1);
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

    private GameObject GetEquipmentPrefab()
    {
        if (equipmentPrefabs != null && equipmentPrefabs.Length > 0)
        {
            return equipmentPrefabs[Random.Range(0, equipmentPrefabs.Length)];
        }
        return null;
    }
}
