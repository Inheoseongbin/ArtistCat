using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Boss : PoolableMono
{
    [Header("적 설정")]
    public List<LineType> enemyTypes;
    public int typeCount;
    private BossType type;
    private int count;
    public GameObject[] exp;

    [SerializeField] private SpriteRenderer _sr;
	[SerializeField] private Animator ani;
    [SerializeField] private int _dieCount;

    [Header("UI 설정")]
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject imageParent;
    public GameObject img;

    [SerializeField] private GameObject BossHpCount;

    private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
    private List<GameObject> typeList = new List<GameObject>();
    private Sprite sprite;

    private readonly int _dissolve = Shader.PropertyToID("_Dissolve");
    private readonly string _isDissolve = "_IsDissolve";
    private readonly string _isHit = "_IsSolidColor";

    [SerializeField] private GameObject _dashImage;

    private int dieCount = 5;

    private GameObject saveTypeCount = null;

    BossSkill bossSkill;

    private Light2D _light;
    private CapsuleCollider2D _hitAble;

    public override void Init()
    {
        _hitAble.enabled = true;
        _light.enabled = true;

        dieCount = _dieCount;

        if (saveTypeCount != null)
            saveTypeCount.GetComponent<TextMeshPro>().text = $"x{dieCount}";

        type = EnemySpawner.Instance.bossTypes;

        imageParent.SetActive(true);

        DeleteEnemy();
        RenderControl();

        EnemySpawner.Instance.isSpawnLock = false;
        EnemySpawner.Instance.isBossDead = false;


        count = typeCount;
        bossSkill.Attack();
    }

    private void RenderControl()
    {
        //적 컬러
        _sr.color = type._BossColor;
        ani.runtimeAnimatorController = type.animator;

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
        saveTypeCount = Instantiate(BossHpCount, imageParent.transform);
        saveTypeCount.GetComponent<TextMeshPro>().text = $"x{dieCount}";
    }

    private void Awake()
    {
        _hitAble = GetComponent<CapsuleCollider2D>();
        _light = GetComponentInChildren<Light2D>();
        bossSkill = GetComponent<BossSkill>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        ani = GetComponentInChildren<Animator>();

        for (int i = 0; i < sprites.Count; i++) // 처음에 딕셔너리에 타입이랑 그림 넣어
        {
            showType.Add((LineType)i + 1, sprites[i]);
        }
    }

    private void Update()
    {
        if (enemyTypes.Count == 0 && !EnemySpawner.Instance.isBossDead) // 남은 타입이 없으면 다 없앴으니까 죽일거임
        {
            Destroy(saveTypeCount);
            dieCount--;
            ReCharging();
            StartCoroutine(Hit());
        }

        if (dieCount == 0 && !EnemySpawner.Instance.isBossDead || Input.GetKeyDown(KeyCode.Q))
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
            SoundManager.Instance.PlayBossHurt();
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
        if (type.Count == 3)
        {
            UIManager.Instance.EndingScene();
        }

        _hitAble.enabled = false;
        _light.enabled = false;

        ObjectActive();

        SoundManager.Instance.PlayBossDie();

        EnemySpawner.Instance.isSpawnLock = true;
        EnemySpawner.Instance.bossSpawn = false;

        EnemySpawner.Instance.isBossDead = true;
        PoolManager.Instance.Push(EnemySpawner.Instance._fence);
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
            Experience r = PoolManager.Instance.Pop(exp[EnemySpawner.Instance.bossTypes.Count - 1].name) as Experience;
            r.transform.position = transform.position;

            Vector3 offset = Random.insideUnitCircle;

            r.transform.DOJump(transform.position + offset, 2, 1, 0.4f);
        }
    }
}
