using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class BossDialogueManager : MonoBehaviour
{
    public static BossDialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    [System.Serializable]
    public class DialoguePhase
    {
        public string speakerName;
        public Sprite speakerIcon;
        [TextArea(3, 5)] public string content;
    }

    public dialoguePanel dialoguePanel;
    string[] content = {"Who is there?", 
                        "How did you get inside my room???",
                        "Why can't I detect you from my eyes?", 
                        "Well nothing, you are just a insect in my eyes.",
                        "Come!! Let me show you ................ hell!"};

    //[Header("Dialogue Settings")]
    //public DialoguePhase[] dialogueSequence; // Dialogue Squence
    //public float defaultTextSpeed = 0.05f; // Text show speed
    //public KeyCode continueKey = KeyCode.Space; // Continue Key

    [Header("Camera Settings")]
    public Camera mainCamera; // 主摄像机
    public Transform playerFocusPoint; // 玩家的镜头焦点位置
    public Transform bossFocusPoint; // Boss的镜头焦点位置
    public float cameraMoveDuration = 1f; // 镜头移动时间
    public float bossZoomLevel = 1.3f; // 聚焦Boss时的镜头缩放级别
    public float playerZoomLevel = 1.3f; // 默认镜头缩放级别

    [Header("UI Settings")]
    public GameObject dialogueUIPrefab; // 对话UI预制件

    private GameObject activeUI; // actived UI
    private Coroutine dialogueRoutine; // 对话协程

    public void StartBossDialogue()
    {
        if (dialogueRoutine != null) return;

        dialogueRoutine = StartCoroutine(DialogueProcess());
    }

    private IEnumerator DialogueProcess()
    {
        // 暂停游戏
        Time.timeScale = 0;
        mainCamera.GetComponent<CameraFollow>().enabled = false;
        //dialoguePanel.dialogueManager.heroKnight.movement = new Vector2(0, 0);

        // 镜头移动到Boss
        yield return StartCoroutine(MoveCamera(
            bossFocusPoint.position,
            bossZoomLevel,
            cameraMoveDuration
        ));

        // 初始化UI
        //activeUI = Instantiate(dialogueUIPrefab);
        //TMP_Text dialogueText = activeUI.GetComponentInChildren<TMP_Text>();
        //Image speakerIcon = activeUI.transform.Find("SpeakerIcon").GetComponent<Image>();

        // 逐段显示对话
        //foreach (var phase in dialogueSequence)
        //{
        //    // 更新UI
        //    speakerIcon.sprite = phase.speakerIcon;
        //    dialogueText.text = "";

        //    // 逐字显示
        //    foreach (char c in phase.content)
        //    {
        //        dialogueText.text += c;
        //        yield return new WaitForSecondsRealtime(defaultTextSpeed);
        //    }

        //    // 等待玩家输入
        //    yield return new WaitUntil(() => Input.GetKeyDown(continueKey));
        //}

        dialoguePanel.dialogueManager.SetSentence(content);
        yield return new WaitUntil(() => dialoguePanel.dialogueManager.isEnd);


        // 清理UI
        //Destroy(activeUI);

        // camera back to player
        yield return StartCoroutine(MoveCamera(
            playerFocusPoint.position,
            playerZoomLevel,
            cameraMoveDuration
        ));

        // Time scale
        Time.timeScale = 1;
        mainCamera.GetComponent<CameraFollow>().enabled = true;
        dialogueRoutine = null;
    }

    private IEnumerator MoveCamera(Vector3 targetPos, float zoom, float duration)
    {
        Vector3 startPos = mainCamera.transform.position;
        float startZoom = mainCamera.orthographicSize;
        float elapsed = 0;

        while (elapsed < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(
                startPos, 
                new Vector3(targetPos.x, targetPos.y, startPos.z), 
                elapsed / duration
            );

            mainCamera.orthographicSize = Mathf.Lerp(
                startZoom, 
                zoom, 
                elapsed / duration
            );

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // 确保最终位置准确
        mainCamera.transform.position = new Vector3(targetPos.x, targetPos.y, startPos.z);
        mainCamera.orthographicSize = zoom;
    }

}