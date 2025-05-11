using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceChest : MonoBehaviour
{
    public GameObject BalancePanel;
    public GameObject moneyBag;

    public BalanceController BalanceManager;

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

    private void Update()
    {
        CheckCorrect();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                BalancePanel.SetActive(true);
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
        if (BalanceManager.isCorrect)
        {
            StartCoroutine(DelayedAction());
            BalanceManager.isCorrect = false; // ¨¾¤î­«½ÆÄ²µo
        }
    }

    private IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(3f); // µ¥«Ý 3 ¬í

        BalancePanel.SetActive(false);
        SpawnItems(moneyBag, transform.position, 3);
        GameManager.Instance.OpenChest(chestID);
        Destroy(gameObject);
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
