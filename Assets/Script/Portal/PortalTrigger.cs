using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public GameObject levelSelectUI; // 关联的UI Panel
    public Animator portalAnimator;  // 传送门动画组件

    private bool playerInRange;
    private HeroKnight player;

    private void Start()
    {
        portalAnimator.enabled = false; // 初始禁用动画
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<HeroKnight>();
            playerInRange = true;

            // 如果玩家有能量球，激活动画
            if (player.hasEnergyBall)
            {
                portalAnimator.enabled = true;
                portalAnimator.Play("Activate"); // 播放激活动画
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            levelSelectUI.SetActive(false); // 离开时关闭UI
        }
    }

    private void Update()
    {
        if (playerInRange && player.hasEnergyBall && Input.GetKeyDown(KeyCode.X))
        {
            levelSelectUI.SetActive(true); // 显示关卡选择UI
            Time.timeScale = 0f; // 暂停游戏
        }
    }

    // 供UI按钮调用的传送方法
    public void TeleportToLevel(int levelIndex, Vector3 spawnPosition)
    {
        Time.timeScale = 1f; // 恢复时间
        SceneManager.LoadScene(levelIndex);
        // 假设新场景有脚本处理玩家位置
        PlayerSpawner.SetSpawnPosition(spawnPosition);
    }
}