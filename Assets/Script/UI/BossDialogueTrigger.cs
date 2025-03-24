using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueTrigger : MonoBehaviour
{
    public Camera m_camera;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BossDialogueManager.Instance.StartBossDialogue();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private IEnumerator MoveCameraToBoss()
    {
        
        yield return new WaitForSeconds(3f);
    }
}
