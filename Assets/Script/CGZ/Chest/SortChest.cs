using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortChest : MonoBehaviour
{
    public GameObject SortQuestionPanel;
    public GameObject moneyBag;
    public GameObject[] equipmentPrefabs;
    public string chestID;

    [SerializeField]
    private NumberSortingManager numberSortingManager;

    public GameObject prompt;

    private void Start()
    {
        chestID = gameObject.name;
        prompt = transform.GetChild(0).gameObject;
        if (prompt != null)
        {
            prompt.SetActive(false);
        }
        numberSortingManager = SortQuestionPanel.GetComponent<NumberSortingManager>();
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
                SortQuestionPanel.SetActive(true);
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

    public void CheckSorted()
    {
        if (numberSortingManager.isSorted == true)
        {
            numberSortingManager.isSorted = false;
            SortQuestionPanel.SetActive(false);
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
                    Random.Range(-6f, -4f)
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
