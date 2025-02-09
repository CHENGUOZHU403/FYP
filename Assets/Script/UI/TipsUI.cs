using UnityEngine;
using UnityEngine.UI;

public class TipsUI : MonoBehaviour
{
    public GameObject promptUI; 
    public Animator promptAnimator; 
    public AudioSource promptSound; 
    private bool isPlayerNear = false;

    private void Start()
    {

        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (promptUI != null)
            {
                promptUI.SetActive(true);
                promptAnimator.SetBool("isVisible", true); 
                if (promptSound != null) promptSound.Play(); 
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (promptAnimator != null)
            {
                promptAnimator.SetBool("isVisible", false); 
            }
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Gei it!");
            Destroy(gameObject); 
        }
    }


}
