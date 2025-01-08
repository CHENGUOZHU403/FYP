using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DamageDisplay : MonoBehaviour
{
    public Text damageText;
    public float displayDuration = 1f;
    public float moveSpeed = 1f;
    public float verticalOffset = 1f;

    public void ShowDamage(Vector3 targetWorldPosition, int damageAmount, float horizontalOffset)
    {
        
        damageText.text = damageAmount.ToString();
        gameObject.SetActive(true);

        Vector3 damagePosition = targetWorldPosition + new Vector3(horizontalOffset, verticalOffset, 0);
        gameObject.transform.position = damagePosition; ;

        StartCoroutine(MoveDamageText());
    }

    private IEnumerator MoveDamageText()
    {

        Vector3 startPosition = gameObject.transform.position; 
        Vector3 targetPosition = startPosition + new Vector3(0, 1f, 0); 

        float elapsedTime = 0f;

        while (elapsedTime < displayDuration)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / displayDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        gameObject.transform.position = targetPosition;

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
