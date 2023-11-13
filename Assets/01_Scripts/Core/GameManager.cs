using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineType
{
    NONE = 0,
    WIDTH = 1,
    LENGTH = 2,
    V = 3,
    REVERSEV = 4,
    THUNDER = 5,
    END
}
public struct SkillInclude
{
    public string name;
    public string info;
    public Sprite image;
}
public class GameManager : MonoBehaviour
{
    private int currentEnemyKills = 0;
    public int EnemyKill => currentEnemyKills;

    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    public static GameManager Instance;

    [HideInInspector] public Camera mainCam;
    public Transform playerTrm;

    public PoolingListSO poolingListSO;

    private float currentPlayTime = 0f;
    public float CurrentPlayTime => currentPlayTime; // 나중에 일정 시간 지나면 보스 불러올 때 쓰는 함수
    private float endTime = 0f;
    public float EndTime => endTime;

    public bool isTimeStop = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        mainCam = Camera.main;
        playerTrm = GameObject.Find("Player").transform;

        currentEnemyKills = 0;
        currentPlayTime = 0;

        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        poolingListSO.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }

    private void Update()
    {
        if (!isTimeStop)
            currentPlayTime += Time.deltaTime;
    }

    public void AddEnemy()
    {
        ++currentEnemyKills;
    }

    public void GameOver()
    {
        endTime = currentPlayTime;
        isGameOver = true;
    }
}
