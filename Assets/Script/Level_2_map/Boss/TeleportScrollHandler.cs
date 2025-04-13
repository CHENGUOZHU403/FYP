// using UnityEngine;
// using UnityEngine.SceneManagement;
// using TMPro;

// public class TeleportScrollHandler : MonoBehaviour
// {
//     public KeyCode teleportKey = KeyCode.B;
//     public GameObject uiPrompt; // 需要包含TMP_Text组件的UI对象
//     public string townSceneName = "MainTown";
    
//     private bool hasScroll = false;
//     private TMP_Text promptText;

//     private void Start()
//     {
//         promptText = uiPrompt.GetComponent<TMP_Text>();
//         uiPrompt.SetActive(false);
//     }

//     private void Update()
//     {
//         if (hasScroll && Input.GetKeyDown(teleportKey))
//         {
//             TeleportToTown();
//         }
//     }

//     public void AcquireScroll()
//     {
//         hasScroll = true;
//         promptText.text = "按 B 键传送回城";
//         uiPrompt.SetActive(true);
//     }

//     private void TeleportToTown()
//     {
//         // 保存游戏状态
//         PlayerPrefs.SetInt("LastBossDefeated", 1);
//         PlayerPrefs.Save();
        
//         SceneManager.LoadScene(townSceneName);
//     }
// }