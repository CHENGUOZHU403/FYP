using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip buttonClickSound; // 用于存储按钮点击声
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 获取AudioSource组件
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound); // 播放点击声
    }
}