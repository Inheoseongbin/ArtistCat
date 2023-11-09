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

    private Transform player;

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
        StartCoroutine(SpawnBoss());
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
            //atOnceCount = Random.Range(1, 2);

            //for (int i = 0; i < atOnceCount; i++)
            //{
            curtime = (int)GameManager.Instance.CurrentPlayTime / 10;
            Enemy e;

            switch (curtime)
            {
                case 0:
                    e = PoolManager.Instance.Pop(enemyList[0].name) as Enemy;
                    break;
                //case 1:
                //    e = PoolManager.Instance.Pop(enemyList[1].name) as Enemy;
                //    break;
                //case 2://º¸½º
                    //break;
                default:
                    e = PoolManager.Instance.Pop(enemyList[0].name) as Enemy;
                    break;
            }

            saveEnemyList.Add(e);

            Vector2 pos = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
            e.transform.position = pos;
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
