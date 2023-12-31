using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class Enemy : PoolableMono
{
    [Header("적 설정")]
    public List<LineType> enemyTypes;
    public int typeCount;
    public int count;
    public GameObject exp;

    [Header("UI 설정")]
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject imageParent;
    public GameObject img;

    private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
    private List<GameObject> typeList = new List<GameObject>();
    private Sprite sprite;

    private SpriteRenderer _sr;
    private Light2D light2D;

    [SerializeField] private SpriteRenderer _emo;
    private readonly int _dissolve = Shader.PropertyToID("_Dissolve");
    private readonly string _isDissolve = "_IsDissolve";
    private readonly string _isHit = "_IsSolidColor";

    public bool _isDead = false;
    private CapsuleCollider2D _hitDecision;

    private Player player;

    public override void Init()
    {
        typeList.Clear();
        enemyTypes.Clear();
        _hitDecision.enabled = true;
        light2D.enabled = true;
        imageParent.SetActive(true);
        _isDead = false;

        foreach (Transform child in imageParent.transform)
        {
            Destroy(child.gameObject);
        }

        //쉐이더 값 초기화
        _sr.material.SetInt(_isHit, 0);
        _sr.material.SetInt(_isDissolve, 0);
        _emo.material.SetInt(_isDissolve, 0);

        _sr.material.SetFloat(_dissolve, 1f);
        _emo.material.SetFloat(_dissolve, 1f);

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

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _hitDecision = GetComponent<CapsuleCollider2D>();
        light2D = GetComponentInChildren<Light2D>();
        player = FindObjectOfType<Player>();

        for (int i = 0; i < sprites.Count; i++) // 처음에 딕셔너리에 타입이랑 그림 넣어
        {
            showType.Add((LineType)i + 1, sprites[i]);
        }
    }

    private void Update()
    {
        if (enemyTypes.Count == 0 && !_isDead) // 남은 타입이 없으면 다 없앴으니까 죽일거임
        {
            Die();
        }
    }

    public void PlayerDraw(LineType attack)
    {
        if (enemyTypes.Count > 0 || !_isDead)
        {
            if (enemyTypes[0] == attack && !_isDead && enemyTypes.Count != 0) // 딕셔너리 타입이랑 첫번째꺼의 타입이 같으면 하나 지울거얌
            {
                DrawReduce(0);
                SoundManager.Instance.PlayEnemyHurt();
            }
        }
    }

    public void DrawReduce(int id)
    {
        if (enemyTypes.Count > 0) // 여러개의 공격을 한번에 받아서 지울게 하나밖에 없을때 
        {
            player.combo++;
            enemyTypes.RemoveAt(id);
            Destroy(typeList[id]);
            typeList.RemoveAt(id);
            StartCoroutine(Hit());
        }
        else
            return;
    }

    private IEnumerator Hit()
    {
        _sr.material.SetInt(_isHit, 1);
        yield return new WaitForSeconds(.1f);
        _sr.material.SetInt(_isHit, 0);
    }


    public void Die()
    {
        SoundManager.Instance.PlayEnemyDie();
        StartCoroutine(DieDissolve(1));
        _isDead = true;
        light2D.enabled = false;
        imageParent.SetActive(false);
        _hitDecision.enabled = false;
        FallExp();
    }

    private IEnumerator DieDissolve(float time)
    {
        _sr.material.SetInt(_isDissolve, 1);
        _emo.material.SetInt(_isDissolve, 1);
        float currentRate;
        float percent = 0;
        float currentTime = 0;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            currentRate = Mathf.Lerp(1, -1, percent);
            _sr.material.SetFloat(_dissolve, currentRate);
            _emo.material.SetFloat(_dissolve, currentRate);

            yield return null;
        }
        PoolManager.Instance.Push(this); // 죽으면 풀링 넣기
        GameManager.Instance.AddEnemy(); // 죽은 에너미
    }

    private void FallExp() // Exp 떨구기
    {
        Experience r = PoolManager.Instance.Pop(exp.name) as Experience;
        r.transform.position = transform.position;

        Vector3 offset = Random.insideUnitCircle;

        r.transform.DOJump(transform.position + offset, 2, 1, 0.4f);
    }
}
