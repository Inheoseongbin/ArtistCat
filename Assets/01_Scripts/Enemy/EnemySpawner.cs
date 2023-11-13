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

    private int curLevel;

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


    private void Update()
    {
        player = GameManager.Instance.playerTrm;

        minx = player.transform.position.x - range;
        miny = player.transform.position.y - range;
        maxx = player.transform.position.x + range;
        maxy = player.transform.position.y + range;

        //print(curtime);
        curtime = (int)GameManager.Instance.CurrentPlayTime;
        curLevel = Level.Instance.level;

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
            /*//if (curtime <= 10) Level1Enemy();
            //else if (curtime < 20) RandEnemy();
            ////이거 이단 문제
            //if (!_bossSpawn)
            //{
            //    Vector2 bossPos = new Vector2(player.position.x, maxy);
            //    b = PoolManager.Instance.Pop("Boss") as Boss;
            //    b.transform.position = bossPos;
            //    bosstime = curtime + 30;
            //    _bossSpawn = true;
            //    GameManager.Instance.isTimeStop = true;
            //}*/

            float time = Random.Range(1, 3);
            yield return new WaitForSeconds(time);
            if (curLevel < 2)
			{
                EnemySpawn(1);
			}
            else if(curLevel < 4)
			{
                EnemySpawn(2);
			}
            else if(curLevel >  6)
			{
                EnemySpawn(3);
			}
            else if(curLevel > 8)
			{
                EnemySpawn(4);
			}
			else
			{
                if (!GameManager.Instance.isTimeStop)
                {
                    RandEnemy();
                    _bossSpawn = false;
                }
            }
        }
    }

    private void EnemySpawn(int idx)
	{
        int r = Random.Range(0, idx);
        print(r);
        e = PoolManager.Instance.Pop(enemyList[r].name) as Enemy;

        e.transform.position = RandomPos();
    }

    private void RandEnemy()
    {
        randEnemyType = Random.Range(0, enemyList.Count);
        e = PoolManager.Instance.Pop(enemyList[randEnemyType].name) as Enemy;
        saveEnemyList.Add(e);
        e.transform.position = RandomPos();
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

    Vector2 RandomPos()
	{
        Vector2 pos = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
        if(Vector3.Distance(transform.position, pos) < 3)
		{
            print("다시");
            RandomPos();
		}
        return pos;
    }
}
