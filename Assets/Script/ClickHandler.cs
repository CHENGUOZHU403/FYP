using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Transform targetPosition;
    public float popDuration = 1f;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void PopOut()
    {
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        float elapsed = 0f;
        while (elapsed < popDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition.position, elapsed / popDuration);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}

