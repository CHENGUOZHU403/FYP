using System.Collections;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private float displayDuration = 5f;
    
    private Coroutine _autoCloseRoutine;
    private bool _isInTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _isInTrigger = true;
        StartAutoClose();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _isInTrigger = false;
    }

    private void StartAutoClose()
    {
        if (_autoCloseRoutine != null)
        {
            StopCoroutine(_autoCloseRoutine);
        }

        objectiveManager.ToggleObjectivePanel(true);
        
        _autoCloseRoutine = StartCoroutine(AutoCloseProcess());
    }

    private IEnumerator AutoCloseProcess()
    {
        float timer = 0;

        while (timer < displayDuration)
        {
            if (!_isInTrigger)
            {
                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        objectiveManager.ToggleObjectivePanel(false);
        _autoCloseRoutine = null;
    }

    // #if UNITY_EDITOR
    // private void OnDrawGizmos()
    // {
    //     CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
    //     Gizmos.color = new Color(1, 0.5f, 0, 0.3f);
    //     Gizmos.DrawCube(transform.position + (Vector3)collider.offset, collider.size);
    // }
    // #endif
}