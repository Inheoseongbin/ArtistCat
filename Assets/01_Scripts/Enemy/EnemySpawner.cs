using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> enemyList;
    public List<GameObject> bossList;

    [HideInInspector] public List<Enemy> saveEnemyList;

    public bool isSpawnLock = false;

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
    [SerializeField] bool _bossSpawn = false;

    int randEnemyType;

    private Transform player;

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
        StartCoroutine(Spawn());
    }


    private void Level1Enemy()
    {
        e = PoolManager.Instance.Pop(enemyList[0].name) as Enemy;

        Vector2 pos = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
        e.transform.position = pos;
    }

    private void RandEnemy()
    {
        randEnemyType = Random.Range(0, enemyList.Count);
        Vector2 pos = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
        e = PoolManager.Instance.Pop(enemyList[randEnemyType].name) as Enemy;
        saveEnemyList.Add(e);
        e.transform.position = pos;
    }

    private void Update()
    {
        //print(curtime);
        curtime = (int)GameManager.Instance.CurrentPlayTime;

        if (curtime >= bosstime)
        {
            if (!_bossSpawn)
            {
                Vector2 bossPos = new Vector2(player.position.x, maxy);

                b = PoolManager.Instance.Pop("Boss") as Boss;
                b.transform.position = bossPos;
                bosstime = curtime + 10;
                _bossSpawn = true;
                GameManager.Instance.isTimeStop = true;
            }
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            player = GameManager.Instance.playerTrm;

            minx = player.transform.position.x - range;
            miny = player.transform.position.y - range;
            maxx = player.transform.position.x + range;
            maxy = player.transform.position.y + range;

            float time = Random.Range(2, 5);

            if (curtime <= 10) Level1Enemy();
            else if (curtime < 20) RandEnemy();
            else
            {
                ////이거 이단 문제
                //if (!_bossSpawn)
                //{
                //    Vector2 bossPos = new Vector2(player.position.x, maxy);

                //    b = PoolManager.Instance.Pop("Boss") as Boss;
                //    b.transform.position = bossPos;
                //    bosstime = curtime + 30;
                //    _bossSpawn = true;
                //    GameManager.Instance.isTimeStop = true;
                //}

                if (!GameManager.Instance.isTimeStop)
                {
                    RandEnemy();
                    print(time);
                    _bossSpawn = false;
                    //if (curtime >= bosstime)
                    //{
                    //}
                }
            }

            //switch (curtime)
            //{
            //    case 0:
            //        e = PoolManager.Instance.Pop(enemyList[0].name) as Enemy;
            //        break;
            //    case 1:
            //        e = PoolManager.Instance.Pop(enemyList[1].name) as Enemy;
            //        break;
            //    case 2:
            //        if (!_bossSpawn)
            //        {
            //            b = PoolManager.Instance.Pop("Boss") as Boss;
            //            GameManager.Instance.isTimeStop = true;
            //            _bossSpawn = true;
            //        }
            //        break;
            //    case 5:
            //        b = PoolManager.Instance.Pop("Boss") as Boss;
            //        GameManager.Instance.isTimeStop = true;
            //        break;
            //    default:
            //        randEnemyType = Random.Range(0, enemyList.Count);

            //        e = PoolManager.Instance.Pop(enemyList[randEnemyType].name) as Enemy;
            //        break;
            //}

            yield return new WaitForSeconds(time);
        }
    }


    IEnumerator SpawnBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(bSpawnTime);
            Boss e = PoolManager.Instance.Pop("Boss") as Boss;
            Vector2 pos = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
            e.transform.position = pos;
        }
    }
}
