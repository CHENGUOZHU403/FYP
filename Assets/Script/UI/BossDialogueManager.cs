using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Boss Dialogue Config")]
public class BossDialogueConfig : ScriptableObject
{
    [System.Serializable]
    public class DialoguePhase
    {
        public string speakerName;
        public Sprite speakerIcon;
        [TextArea(3, 5)] public string content;
        public Transform focusPoint; 
        public float zoomLevel = 5f;
        public float cameraMoveDuration = 1f;
    }

    public DialoguePhase[] dialogueSequence;
    public AudioClip[] voiceEffects;
    public GameObject dialogueUIPrefab;
}

public class BossDialogueManager : MonoBehaviour
{
    public static BossDialogueManager Instance { get; private set; }

    [Header("Global Settings")]
    public float defaultTextSpeed = 0.05f;
    public KeyCode continueKey = KeyCode.Space;
    public Camera mainCamera;
    public Transform playerFocusPoint;

    private BossDialogueConfig currentConfig;
    private GameObject activeUI;
    private Coroutine dialogueRoutine;

    void Awake()
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

    public void StartBossDialogue(BossDialogueConfig config)
    {
        if (dialogueRoutine != null) return;

        currentConfig = config;
        dialogueRoutine = StartCoroutine(DialogueProcess());
    }

    private IEnumerator DialogueProcess()
    {
        // 初始化UI
        activeUI = Instantiate(currentConfig.dialogueUIPrefab);
        TMP_Text dialogueText = activeUI.GetComponentInChildren<TMP_Text>();
        Image speakerIcon = activeUI.transform.Find("SpeakerIcon").GetComponent<Image>();

        // 暂停游戏
        Time.timeScale = 0;

        foreach (var phase in currentConfig.dialogueSequence)
        {
            // 镜头移动
            yield return StartCoroutine(MoveCamera(
                phase.focusPoint.position,
                phase.zoomLevel,
                phase.cameraMoveDuration
            ));

            // 更新UI
            speakerIcon.sprite = phase.speakerIcon;
            dialogueText.text = "";

            // 逐字显示
            foreach (char c in phase.content)
            {
                dialogueText.text += c;
                yield return new WaitForSecondsRealtime(defaultTextSpeed);
            }

            // 等待玩家输入
            yield return new WaitUntil(() => Input.GetKeyDown(continueKey));
        }

        // 清理
        Destroy(activeUI);
        yield return ReturnCameraToPlayer();
        Time.timeScale = 1;
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
    }

    private IEnumerator ReturnCameraToPlayer()
    {
        yield return MoveCamera(
            playerFocusPoint.position,
            mainCamera.orthographicSize,
            1f
        );
    }

    // 触发示例（挂在Boss房间触发器上）
    public class BossDialogueTrigger : MonoBehaviour
    {
        public BossDialogueConfig dialogueConfig;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                BossDialogueManager.Instance.StartBossDialogue(dialogueConfig);
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}