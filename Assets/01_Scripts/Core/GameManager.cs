using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineType
{
    NONE = 0,
    WIDTH = 1,
    LENGTH,
    V,
    REVERSEV,
    THUNDER,
    END
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
        
        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        poolingListSO.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }

    public void AddEnemy()
    {
        ++currentEnemyKills;
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
