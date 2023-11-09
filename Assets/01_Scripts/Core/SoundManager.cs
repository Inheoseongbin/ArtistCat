using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;
    private Camera cam;

    private string bgmKey = "BGMVolume";
    private string effectKey = "EffectVolume";

    [Header("Player")]
    [SerializeField] private AudioClip playerMoveSound;

    private void Awake()
    {
        if(Instance != null)
        {
            print("사운드매니저에러에러");
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;

        PlayerPrefs.SetFloat(bgmKey, 0.1f);
        PlayerPrefs.SetFloat(effectKey, 0.4f);
    }

    private void Start()
    {
        cam.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(bgmKey);
        audioSource.volume = PlayerPrefs.GetFloat(effectKey);
    }

    // 배경음악은 메인 카메라로 재생

    // 볼륨 조절 함수
    public void SetBGMVolume(float value)
    {
        cam.GetComponent<AudioSource>().volume = value;
        PlayerPrefs.SetFloat(bgmKey, value);
    }
    public void SetEffectVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(effectKey, value);
    }

    // 이런 식으로 쓰면 됨
    // 플레이어
    public void PlayPlayerMoveSound()
    {
        audioSource.PlayOneShot(playerMoveSound);
    }
}
