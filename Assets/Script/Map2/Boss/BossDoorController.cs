using UnityEngine;
using TMPro;

public class BossDoorController : MonoBehaviour
{
    [Header("References")]
    public SceneLoader sceneLoader;
    public Animator doorAnimator;
    public TMP_Text promptText;

    private bool playerInRange;

    private void Start()
    {
       //PuzzleManager.Instance.OnAllPuzzlesSolved += () => {
       //    doorAnimator.SetBool("Unlocked", true);
       //};
    }

    private void Update()
    {
        if (playerInRange && PuzzleManager.Instance.AllPuzzlesSolved)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                sceneLoader.InitiateBossTransition();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PuzzleManager.Instance.AllPuzzlesSolved)
        {
            playerInRange = true;
            promptText.gameObject.SetActive(true);
            sceneLoader.EnableTransition();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptText.gameObject.SetActive(false);
        }
    }
}