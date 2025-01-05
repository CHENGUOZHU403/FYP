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
        // ��ȡ��ʼ��ɫ
        Color maskColor = blackMask.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // �𽥼��� Alpha ֵ
            maskColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackMask.color = maskColor;

            // �ۼ�ʱ��
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȷ����ȫ͸��
        maskColor.a = 0f;
        blackMask.color = maskColor;

        // �������֣��������ܣ���ѡ��
        blackMask.gameObject.SetActive(false);
    }
}
