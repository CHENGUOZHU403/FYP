using UnityEngine;
using TMPro;

public class BossDoorController : MonoBehaviour
{
    [Header("References")]
    public SceneLoader sceneLoader;
    public Animator doorAnimator;
    public TMP_Text promptText;
    public GameObject promptTextContainer;

    public GameObject prompt;

    private bool playerInRange;

    private void Start()
    {
        prompt = transform.GetChild(0).gameObject;
        if (prompt != null)
        {
            prompt.SetActive(false);
        }
        //PuzzleManager.Instance.OnAllPuzzlesSolved += () => {
        //    doorAnimator.SetBool("Unlocked", true);
        //};
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (GameManager.Instance.AllMonsterDefeated)
                {
                    sceneLoader.InitiateBossTransition();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(true);
            playerInRange = true;
            promptTextContainer.SetActive(true);
            if (GameManager.Instance.AllMonsterDefeated)
            {
                promptText.text = "To see the final Boss";
                sceneLoader.EnableTransition();
            }
            else
            {
                promptText.text = "Need to Defeat three monsters";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.SetActive(true);
            playerInRange = true;
            promptTextContainer.SetActive(true);
            if (GameManager.Instance.AllMonsterDefeated)
            {
                promptText.text = "To see the final Boss";
                sceneLoader.EnableTransition();
            }
            else
            {
                promptText.text = "Need to Defeat three monsters";
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (prompt != null)
                prompt.SetActive(false);

            if (promptTextContainer != null)
                promptTextContainer.SetActive(false);
        }
    }
}