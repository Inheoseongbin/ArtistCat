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
    [SerializeField] private AudioClip playerHurtSound;
    [SerializeField] private AudioClip playerDieSound;
    [SerializeField] private AudioClip playerSuccessAtkSound;

    [Header("Level")]
    [SerializeField] private AudioClip selectExpSound;
    [SerializeField] private AudioClip levelUpSound;

    [Header("Enemy")]
    [SerializeField] private AudioClip enemyHurtSound;
    [SerializeField] private AudioClip enemyDieSound;

    [Header("Boss")]
    [SerializeField] private AudioClip bossSpawnSound;
    [SerializeField] private AudioClip bossDashSound;
    [SerializeField] private AudioClip bossShootingSound;
    [SerializeField] private AudioClip bossHurtSound;
    [SerializeField] private AudioClip bossDieSound;

    [Header("UI")]
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip skillUIOnSound;
    [SerializeField] private AudioClip skillUIOffSound;
    [SerializeField] private AudioClip gameFailSound;
    [SerializeField] private AudioClip gameSuccessSound;

    [Header("Skill")]
    [SerializeField] private AudioClip throwingPoopSound;
    [SerializeField] private AudioClip landFishThronSound;
    [SerializeField] private AudioClip rollAroundStringSound;
    [SerializeField] private AudioClip scratchEnemySound;
    [SerializeField] private AudioClip playerHealSound;

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

    // �÷��̾�
    public void PlayPlayerMove()
    {
        audioSource.PlayOneShot(playerMoveSound);
    }

    public void PlayPlayerHurt()
    {
        audioSource.PlayOneShot(playerHurtSound);
    }

    public void PlayPlayerDie()
    {
        audioSource.PlayOneShot(playerDieSound);
    }

    public void PlayPlayerSuccessAtk()
    {
        audioSource.PlayOneShot(playerSuccessAtkSound);
    }

    // ����
    public void PlaySelectExp()
    {
        audioSource.PlayOneShot(selectExpSound);
    }

    public void PlayLevelUp()
    {
        audioSource.PlayOneShot(levelUpSound);
    }

    // ��
    public void PlayEnemyHurt()
    {
        audioSource.PlayOneShot(enemyHurtSound);
    }

    public void PlayEnemyDie()
    {
        audioSource.PlayOneShot(enemyDieSound);
    }

    // ����
    public void PlayBossSpawn()
    {
        audioSource.PlayOneShot(bossSpawnSound);
    }

    public void PlayBossDashAtk()
    {
        audioSource.PlayOneShot(bossDashSound);
    }

    public void PlayBossShootAtk()
    {
        audioSource.PlayOneShot(bossShootingSound);
    }

    public void PlayBossHurt()
    {
        audioSource.PlayOneShot(bossHurtSound);
    }

    public void PlayBossDie()
    {
        audioSource.PlayOneShot(bossDieSound);
    }

    // UI
    public void PlayBTNClick()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void PlaySkillChooseOn() // ��ų UI ������ ��
    {
        audioSource.PlayOneShot(skillUIOnSound);
    }

    public void PlaySkillChooseOff() // ��ų ������ ��
    {
        audioSource.PlayOneShot(skillUIOffSound);
    }

    public void PlayGameFail()
    {
        audioSource.PlayOneShot(gameFailSound);
    }

    public void PlayGameSuccess()
    {
        audioSource.PlayOneShot(gameSuccessSound);
    }

    // ��ų
    public void PlayThrowingPoop()
    {
        audioSource.PlayOneShot(throwingPoopSound);
    }

    public void PlayLandFishThron()
    {
        audioSource.PlayOneShot(landFishThronSound);
    }

    public void PlayRollingString()
    {
        audioSource.PlayOneShot(rollAroundStringSound);
    }

    public void PlayScratchEnemy()
    {
        audioSource.PlayOneShot(scratchEnemySound);
    }

    public void PlayPlayerHeal()
    {
        audioSource.PlayOneShot(playerHealSound);
    }
}
