using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DamageDisplay : MonoBehaviour
{
    public Text damageText;
    public float displayDuration = 1f;
    public float moveSpeed = 20f;
    public float verticalOffset = 10f;
    public float horizontalOffset = 0.5f;

    public void ShowDamage(Vector3 startPosition, int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        gameObject.SetActive(true);
        Debug.Log(gameObject.transform.position);
        
        StartCoroutine(MoveDamageText());
    }

    private IEnumerator MoveDamageText()
    {

        Vector3 startPosition = gameObject.transform.position; 
        Vector3 targetPosition = startPosition + new Vector3(0,100, 0); 

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
