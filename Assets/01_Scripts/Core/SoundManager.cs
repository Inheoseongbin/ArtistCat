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
            print("����Ŵ�����������");
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    // ��������� ���� ī�޶�� ���

    // �̷� ������ ���� ��
    public void PlayPlayerMoveSound()
    {
        audioSource.PlayOneShot(playerMoveSound);
    }
}
