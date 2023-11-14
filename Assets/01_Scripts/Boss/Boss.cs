using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Boss : PoolableMono
{
    [Header("�� ����")]
    public List<LineType> enemyTypes;
    public int typeCount;
    private int count;
    public GameObject exp;

    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private int _dieCount;

    [Header("UI ����")]
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject imageParent;
    public GameObject img;

    private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
    private List<GameObject> typeList = new List<GameObject>();
    private Sprite sprite;

    private readonly int _dissolve = Shader.PropertyToID("_Dissolve");
    private readonly string _isDissolve = "_IsDissolve";
    private readonly string _isHit = "_IsSolidColor";

    private int dieCount = 5;
    private bool _isDead = false;

    BossSkill bossSkill;

    public override void Init()
    {
        DeleteEnemy();
        CreateFence();

        EnemySpawner.Instance.isSpawnLock = false;
        _isDead = false;

        dieCount = _dieCount;

        //���̴� �� �ʱ�ȭ
        _sr.material.SetInt(_isHit, 0);
        _sr.material.SetInt(_isDissolve, 0);

        _sr.material.SetFloat(_dissolve, 1f);

        count = typeCount;
        bossSkill.Attack();
    }

    private void CreateFence()
	{
        Fence _fence = PoolManager.Instance.Pop("Fence") as Fence;
        _fence.transform.position = transform.position;
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
        for (int i = 0; i < count; i++) // ���� ������ŭ �������� �ָ��� ���� Ÿ�� �޾��ֱ�
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

        for (int i = 0; i < sprites.Count; i++) // ó���� ��ųʸ��� Ÿ���̶� �׸� �־�
        {
            showType.Add((LineType)i + 1, sprites[i]);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (enemyTypes.Count == 0 && !_isDead) // ���� Ÿ���� ������ �� �������ϱ� ���ϰ���
        {
            dieCount--;
            ReCharging();
            StartCoroutine(Hit());
        }

        if (dieCount == 0 && !_isDead)
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
        if (enemyTypes[0] == attack) // ��ųʸ� Ÿ���̶� ù��°���� Ÿ���� ������ �ϳ� ����ž�
        {
            enemyTypes.RemoveAt(0);
            Destroy(typeList[0]);
            typeList.RemoveAt(0);
        }
    }


    public void DrawReduce(int id)
    {
        if (enemyTypes.Any()) // �������� ������ �ѹ��� �޾Ƽ� ����� �ϳ��ۿ� ������ 
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
        EnemySpawner.Instance.isSpawnLock = true;
        EnemySpawner.Instance._bossSpawn = false;

        _isDead = true;
        print(Fence.bossDie);
        Fence.bossDie();
        GameManager.Instance.isTimeStop = false;
        StartCoroutine(DieDissolve(1));
        FallExp();
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
        PoolManager.Instance.Push(this); // ������ Ǯ�� �ֱ�
    }

    private void FallExp() // Exp ������
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
