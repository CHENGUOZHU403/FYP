using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public GameObject levelSelectUI;
    public Animator portalAnimator; 

    private bool playerInRange;
    private HeroKnight player;

    private int selectedLevelIndex;
    private Vector3 selectedSpawnPosition;

    private void Start()
    {
        portalAnimator.enabled = false; 
        levelSelectUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<HeroKnight>();
            playerInRange = true;


            if (player.hasEnergyBall)
            {
                portalAnimator.enabled = true;
                portalAnimator.Play("Activate"); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            levelSelectUI.SetActive(false); 
        }
    }

    private void Update()
    {
        if (playerInRange && player.hasEnergyBall && Input.GetKeyDown(KeyCode.X))
        {
            levelSelectUI.SetActive(true); 
            Time.timeScale = 0f; 
        }
    }

    public void SetLevelIndex(int index)
    {
        selectedLevelIndex = index;
    }

    public void SetSpawnPosition(Vector3 pos)
    {
        selectedSpawnPosition = pos;
    }

    public void ExecuteTeleport()
    {
        if (selectedLevelIndex < 0) return;

        Time.timeScale = 1f;
        PlayerSpawner.SetSpawnPosition(selectedSpawnPosition);
        SceneManager.LoadScene(selectedLevelIndex);
    }
    
    public void CloseLevelSelectUI()
    {
        levelSelectUI.SetActive(false);
        Time.timeScale = 1f; 
    }

}