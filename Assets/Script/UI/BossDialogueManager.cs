using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class BossDialogueManager : MonoBehaviour
{
    public static BossDialogueManager Instance { get; private set; }

    [System.Serializable]
    public class DialoguePhase
    {
        public string speakerName; // 说话者名字
        public Sprite speakerIcon; // 说话者头像
        [TextArea(3, 5)] public string content; // 对话内容
    }

    [Header("Dialogue Settings")]
    public DialoguePhase[] dialogueSequence; // 对话序列
    public float defaultTextSpeed = 0.05f; // 文字显示速度
    public KeyCode continueKey = KeyCode.Space; // 继续对话的按键

    [Header("Camera Settings")]
    public Camera mainCamera; // 主摄像机
    public Transform playerFocusPoint; // 玩家的镜头焦点位置
    public Transform bossFocusPoint; // Boss的镜头焦点位置
    public float cameraMoveDuration = 1f; // 镜头移动时间
    public float bossZoomLevel = 5f; // 聚焦Boss时的镜头缩放级别
    public float playerZoomLevel = 7f; // 默认镜头缩放级别

    [Header("UI Settings")]
    public GameObject dialogueUIPrefab; // 对话UI预制件

    private GameObject activeUI; // 当前激活的UI
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

        // 镜头移动到Boss
        yield return StartCoroutine(MoveCamera(
            bossFocusPoint.position,
            bossZoomLevel,
            cameraMoveDuration
        ));

        // 初始化UI
        activeUI = Instantiate(dialogueUIPrefab);
        TMP_Text dialogueText = activeUI.GetComponentInChildren<TMP_Text>();
        Image speakerIcon = activeUI.transform.Find("SpeakerIcon").GetComponent<Image>();

        // 逐段显示对话
        foreach (var phase in dialogueSequence)
        {
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

        // 清理UI
        Destroy(activeUI);

        // 镜头回到玩家
        yield return StartCoroutine(MoveCamera(
            playerFocusPoint.position,
            playerZoomLevel,
            cameraMoveDuration
        ));

        // 恢复游戏
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

        // 确保最终位置准确
        mainCamera.transform.position = new Vector3(targetPos.x, targetPos.y, startPos.z);
        mainCamera.orthographicSize = zoom;
    }

}