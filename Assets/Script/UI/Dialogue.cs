using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Button nextButton;

    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines;

    public float textDisplaySpeed = 0.05f;
    private int currentLineIndex = 0;
    private Coroutine displayCoroutine;

    public Image blackMask;
    public float fadeDuration = 2.0f;

    private void Start()
    {
        StartDialogue();
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    private void StartDialogue()
    {
        currentLineIndex = 0;
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }

        if (currentLineIndex < dialogueLines.Length)
        {
            displayCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
            StartCoroutine(FadeIn());
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDisplaySpeed);
        }
    }

    private IEnumerator FadeIn()
    {
        // 获取初始颜色
        Color maskColor = blackMask.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // 逐渐减少 Alpha 值
            maskColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackMask.color = maskColor;

            // 累计时间
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保完全透明
        maskColor.a = 0f;
        blackMask.color = maskColor;

        // 禁用遮罩，提升性能（可选）
        blackMask.gameObject.SetActive(false);
    }
}
