using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortalTrigger : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Near Portal");
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.Instance.ReturnFromTown();
            }
        }
    }
}
