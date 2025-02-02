using UnityEngine;
using UnityEngine.UI;

public class TipsUI : MonoBehaviour
{
    public GameObject promptUI; // UI 提示
    public Animator promptAnimator; // Animator 控制動畫
    public AudioSource promptSound; // 音效

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
                promptAnimator.SetBool("isVisible", true); // 播放動畫
                if (promptSound != null) promptSound.Play(); // 播放音效
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
                promptAnimator.SetBool("isVisible", false); // 關閉動畫
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
