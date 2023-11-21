using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] private BossType[] saveBossTypes;

    public List<GameObject> enemyList;
    public List<GameObject> bossList;

    [HideInInspector] public List<Enemy> saveEnemyList;

    public bool isSpawnLock = true;
    public bool isBossDead = false;

    public BossType bossTypes;

    [SerializeField] private float range;
    [SerializeField] private float bSpawnTime;
    [SerializeField] private int eSpawnTimeMax;
    [SerializeField] private int eSpawnTimeMin;

    private int atOnceCount;
    private float minx;
    private float miny;
    private float maxx;
    private float maxy;
    private int curtime;

    [SerializeField] private int bosstime = 20;
    [SerializeField] private int nextTime = 20;
    [SerializeField] private float warningTime = 2;

    [SerializeField] private TextMeshProUGUI warningText;

    public bool bossSpawn = false;

    private Transform player;

    private int randEnemyType;
    private int curLevel;

    private bool isTweenkle = false;

    private int bossTypeCount = 0;

    //이거 일단 문제
    Enemy e = null;
    Boss b = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Spawner is running! Check!");
        }
        Instance = this;
    }

    private void Start()
    {
        bossTypes = saveBossTypes[0];

        StartCoroutine(Spawn());
    }


    private void Update()
    {
        player = GameManager.Instance.playerTrm;

        minx = player.transform.position.x - range - 5;
        miny = player.transform.position.y - range;
        maxx = player.transform.position.x + range + 5;
        maxy = player.transform.position.y + range;

        curtime = (int)GameManager.Instance.CurrentPlayTime;
        curLevel = Level.Instance.level;

        if (curtime == bosstime - 2 && !isTweenkle)
        {
            WarningText();
            isTweenkle = true;
        }

        BossSpawn();
    }

    private void BossSpawn()
    {
        if (curtime >= bosstime)
        {
            if (!bossSpawn)
            {
                bossTypes = saveBossTypes[bossTypeCount++];

                Vector2 bossPos = new Vector2(player.position.x, maxy);

                b = PoolManager.Instance.Pop("Boss") as Boss;
                b.transform.position = bossPos;
                CreateFence();
                bosstime = curtime + nextTime;
                bossSpawn = true;
                isTweenkle = false;
                GameManager.Instance.isTimeStop = true;
            }
        }
    }

    private void CreateFence()
    {
        Fence _fence = PoolManager.Instance.Pop("Fence") as Fence;
        _fence.transform.position = player.position;
    }

    private void EnemySpawn(int idx)
    {
        int r = Random.Range(0, idx);
        e = PoolManager.Instance.Pop(enemyList[r].name) as Enemy;

        saveEnemyList.Add(e);
        e.transform.position = RandomPos();
    }

    private void RandEnemy()
    {
        randEnemyType = Random.Range(0, enemyList.Count);
        e = PoolManager.Instance.Pop(enemyList[randEnemyType].name) as Enemy;

        saveEnemyList.Add(e);
        e.transform.position = RandomPos();
    }

    private void WarningText()
    {
        TMPTextTyping(1f);
        warningText.rectTransform.DOShakePosition(3, 30);
        warningText.DOFade(1, 0.5f).SetLoops(6, LoopType.Yoyo);
    }

    private void TMPTextTyping(float time) // 타이핑 해주는 거
    {
        warningText.maxVisibleCharacters = 0;
        DOTween.To(x => warningText.maxVisibleCharacters = (int)x, 0f, warningText.text.Length, time);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            float time = Random.Range(1, 3);
            yield return new WaitForSeconds(time);
            if (isSpawnLock)
            {
                if (curLevel < 2)
                {
                    EnemySpawn(1);
                }
                else if (curLevel < 4)
                {
                    EnemySpawn(2);
                }
                else if (curLevel > 6)
                {
                    EnemySpawn(3);
                }
                else if (curLevel > 8)
                {
                    EnemySpawn(4);
                }
                else
                {
                    if (!GameManager.Instance.isTimeStop)
                    {
                        RandEnemy();
                    }
                }
            }
            yield return null;
        }
    }

    Vector2 RandomPos()
    {
        Vector2 pos = new Vector2(Random.Range(miny - 5, maxy + 5), Random.Range(minx - 5, maxx + 5));
        if (Vector3.Distance(player.transform.position, pos) < 15)
        {
            RandomPos();
        }
        return pos;
    }
}
