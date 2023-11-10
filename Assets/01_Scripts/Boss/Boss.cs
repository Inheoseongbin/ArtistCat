using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : PoolableMono
{
    [Header("적 설정")]
    public List<LineType> enemyTypes;
    public int typeCount;
    private int count;
    public GameObject exp;

    [SerializeField] private SpriteRenderer _sr;

    [Header("UI 설정")]
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject imageParent;
    public GameObject img;

    private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
    private List<GameObject> typeList = new List<GameObject>();
    private Sprite sprite;

    private int dieCount = 5;

    public override void Init()
    {
        _sr.material.SetInt("_IsSolidColor", 0);

        count = typeCount;
        for (int i = 0; i < count; i++) // 이제 개수만큼 랜덤으로 애마다 공격 타입 받아주기
        {
            int r = Random.Range(1, (int)LineType.END);
            enemyTypes.Add((LineType)r);

            if (showType.TryGetValue((LineType)r, out sprite))
            {
                sprites.Add(sprite);
                GameObject g = Instantiate(img, imageParent.transform);
                g.GetComponent<Image>().sprite = sprite;
                typeList.Add(g);
            }
        }
    }

    private void ReCharging()
    {
        for (int i = 0; i < count; i++) // 이제 개수만큼 랜덤으로 애마다 공격 타입 받아주기
        {
            int r = Random.Range(1, (int)LineType.END);
            enemyTypes.Add((LineType)r);

            if (showType.TryGetValue((LineType)r, out sprite))
            {
                sprites.Add(sprite);
                GameObject g = Instantiate(img, imageParent.transform);
                g.GetComponent<Image>().sprite = sprite;
                typeList.Add(g);
            }
        }
    }

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();

        for (int i = 0; i < sprites.Count; i++) // 처음에 딕셔너리에 타입이랑 그림 넣어
        {
            showType.Add((LineType)i + 1, sprites[i]);
        }
    }

    private void Start()
    {
        var saveEnemy = EnemySpawner.Instance.saveEnemyList;

        foreach (Enemy enemy in saveEnemy)
            PoolManager.Instance.Push(enemy);
    }

    private void Update()
    {
        if (enemyTypes.Count == 0) // 남은 타입이 없으면 다 없앴으니까 죽일거임
        {
            dieCount--;
            ReCharging();
            StartCoroutine(Hit());
        }

        if (dieCount == 0)
            Die();
    }

    private IEnumerator Hit()
    {
        _sr.material.SetInt("_IsSolidColor", 1);
        yield return new WaitForSeconds(.1f);
        _sr.material.SetInt("_IsSolidColor", 0);
    }

    public void PlayerDraw(LineType attack)
    {
        if (enemyTypes[0] == attack) // 딕셔너리 타입이랑 첫번째꺼의 타입이 같으면 하나 지울거얌
        {
            enemyTypes.RemoveAt(0);
            Destroy(typeList[0]);
            typeList.RemoveAt(0);
        }
    }

    public void Die()
    {
        GameManager.Instance.isTimeStop = false;
        Fence.bossDie();
        PoolManager.Instance.Push(this); // 죽으면 풀링 넣기
        FallExp();
    }

    private void FallExp() // Exp 떨구기
    {
        int randNum = Random.Range(4, 8);

        for (int i = 0; i < randNum; i++)
        {
            Experience r = PoolManager.Instance.Pop(exp.name) as Experience;
            r.transform.position = transform.position;

            Vector3 offset = Random.insideUnitCircle;

            r.transform.DOJump(transform.position + offset, 2, 1, 0.4f);
        }
    }
}
