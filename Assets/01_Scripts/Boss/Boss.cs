using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Boss : PoolableMono
{
    [Header("적 설정")]
    public List<LineType> enemyTypes;
    public int typeCount;
    private BossType type;
    private int count;
    public GameObject exp;

    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private int _dieCount;

    [Header("UI 설정")]
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject imageParent;
    public GameObject img;

    private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
    private List<GameObject> typeList = new List<GameObject>();
    private Sprite sprite;

    private readonly int _dissolve = Shader.PropertyToID("_Dissolve");
    private readonly string _isDissolve = "_IsDissolve";
    private readonly string _isHit = "_IsSolidColor";

    [SerializeField] private GameObject _dashImage;

    private int dieCount = 5;
    private int maxCount;

    BossSkill bossSkill;

    public override void Init()
    {
        BossHpBar.ShowUiBar();

        type = EnemySpawner.Instance.bossTypes;

        imageParent.SetActive(true);

        DeleteEnemy();
        RenderControl();

        EnemySpawner.Instance.isSpawnLock = false;
        EnemySpawner.Instance.isBossDead = false;

        dieCount = _dieCount;

        count = typeCount;
        bossSkill.Attack();
    }

    private void RenderControl()
    {
        //적 컬러
        _sr.color = type._BossColor;

        //쉐이더 값 초기화
        _sr.material.SetInt(_isHit, 0);
        _sr.material.SetInt(_isDissolve, 0);

        _sr.material.SetFloat(_dissolve, 1f);
    }

    private void DeleteEnemy()
	{
        var saveEnemy = EnemySpawner.Instance.saveEnemyList;

        foreach (Enemy enemy in saveEnemy)
        {
            PoolManager.Instance.Push(enemy);
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
        bossSkill = GetComponent<BossSkill>();
        _sr = GetComponentInChildren<SpriteRenderer>();

        for (int i = 0; i < sprites.Count; i++) // 처음에 딕셔너리에 타입이랑 그림 넣어
        {
            showType.Add((LineType)i + 1, sprites[i]);
        }
    }

    private void Start()
    {
        maxCount = dieCount * typeCount;
    }

    private void Update()
    {
        UIManager.Instance._bossHp.UpdateExpBar(dieCount * typeCount, maxCount - typeCount);

        if (enemyTypes.Count == 0 && !EnemySpawner.Instance.isBossDead) // 남은 타입이 없으면 다 없앴으니까 죽일거임
        {
            dieCount--;
            ReCharging();
            StartCoroutine(Hit());
        }

        if (dieCount == 0 && !EnemySpawner.Instance.isBossDead)
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
        else
            return;
    }


    public void DrawReduce(int id)
    {
        if (enemyTypes.Any()) // 여러개의 공격을 한번에 받아서 지울게 하나밖에 없을때 
        {
            enemyTypes.RemoveAt(id);
            Destroy(typeList[id]);
            typeList.RemoveAt(id);
            StartCoroutine(Hit());
        }
        else
            return;
    }

    public void Die()
    {
        ObjectActive();

        BossHpBar.ShowUiBar();
        EnemySpawner.Instance.isSpawnLock = true;
        EnemySpawner.Instance.bossSpawn = false;

        EnemySpawner.Instance.isBossDead = true;
        Fence.bossDie();
        GameManager.Instance.isTimeStop = false;
        StartCoroutine(DieDissolve(1));
        FallExp();
    }

    private void ObjectActive()
    {
        imageParent.SetActive(false);
        _dashImage.SetActive(false);
    }

    private IEnumerator DieDissolve(float time)
    {
        _sr.material.SetInt(_isDissolve, 1);
        float currentRate;
        float percent = 0;
        float currentTime = 0;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            currentRate = Mathf.Lerp(1, -1, percent);
            _sr.material.SetFloat(_dissolve, currentRate);

            yield return null;
        }
        PoolManager.Instance.Push(this); // 죽으면 풀링 넣기
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
