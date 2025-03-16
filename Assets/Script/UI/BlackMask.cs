using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMask : MonoBehaviour
{
    [Header("Black Mask")]
    public Image blackMask;
    public float fadeDuration = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.isWatched)
        {
            blackMask.enabled = false;
        }
        else
        {
            GameManager.Instance.isWatched = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FadeIn()
    {
        Color maskColor = blackMask.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            maskColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackMask.color = maskColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        maskColor.a = 0f;
        blackMask.color = maskColor;

        blackMask.gameObject.SetActive(false);
    }

    public void StratFade()
    {
        StartCoroutine(FadeIn());
    }
}
