using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortChest : MonoBehaviour
{
    public GameObject SortQuestionPanel;
    public GameObject moneyBag;

    [SerializeField]
    private NumberSortingManager numberSortingManager;

    private void Start()
    {
        numberSortingManager = SortQuestionPanel.GetComponent<NumberSortingManager>();
    }

    private void Update()
    {
        if (numberSortingManager.isSorted == true)
        {
            SortQuestionPanel.SetActive(false);
            SpawnItems(moneyBag, gameObject.transform.position, 3);
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
                    Random.Range(2f, 4f)
                ), ForceMode2D.Impulse);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            if (Input.GetKey(KeyCode.F))
            {
                SortQuestionPanel.SetActive(true);
            }
        }
    }

    
}
