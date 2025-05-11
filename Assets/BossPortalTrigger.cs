using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortalTrigger : MonoBehaviour
{
    public GameObject prompt;
    private void Start()
    {
        prompt = transform.GetChild(0).gameObject;
        if (prompt != null)
        {
            prompt.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.SetActive(true);
            Debug.Log("near");
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.Instance.ReturnFromTown();
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
}
