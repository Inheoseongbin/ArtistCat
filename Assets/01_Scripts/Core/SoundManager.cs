using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static SoundManager Instance;

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
    }

    // 배경음악은 메인 카메라로 재생

    // 이런 식으로 쓰면 됨
    public void PlayPlayerMoveSound()
    {
        audioSource.PlayOneShot(playerMoveSound);
    }
}
