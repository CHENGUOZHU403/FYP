using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueTrigger : MonoBehaviour
{
    public Camera m_camera;
    public Transform targetPosition;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BossDialogueManager.Instance.StartBossDialogue();
            //m_camera.GetComponent<CameraFollow>().enabled = false;
            //m_camera.transform.position = targetPosition.position;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private IEnumerator MoveCameraToBoss()
    {
        
        yield return new WaitForSeconds(3f);
    }
}
