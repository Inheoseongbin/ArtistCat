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
            print("����Ŵ�����������");
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

    // ��������� ���� ī�޶�� ���

    // ���� ���� �Լ�
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

    // �̷� ������ ���� ��
    // �÷��̾�
    public void PlayPlayerMoveSound()
    {
        audioSource.PlayOneShot(playerMoveSound);
    }
}
