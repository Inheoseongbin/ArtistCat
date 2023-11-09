using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class Enemy : PoolableMono
{
    [Header("�� ����")]
    public List<LineType> enemyTypes;
    public int typeCount;
    private int count;
    public GameObject exp;

    [Header("UI ����")]
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject imageParent;
    public GameObject img;

    private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
    private List<GameObject> typeList = new List<GameObject>();
    private Sprite sprite;

    private SpriteRenderer _sr;
    [SerializeField] private SpriteRenderer _emo;
    private readonly int _dissolve = Shader.PropertyToID("_Dissolve");
    private readonly string _isDissolve = "_IsDissolve";
    private readonly string _isHit = "_IsSolidColor";

    public bool _isDead = false;
    private BoxCollider2D _hitDecision;

    public override void Init()
    {
        _emo.enabled = true;
        _sr.material.SetInt(_isHit, 0);
        _sr.material.SetInt(_isDissolve, 0);
        _hitDecision.enabled = true;
        _isDead = false;

        //���̴� �� �ʱ�ȭ
        _sr.material.SetFloat(_dissolve, 1f);

        count = typeCount;
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
        _sr = GetComponentInChildren<SpriteRenderer>();
        _hitDecision = GetComponent<BoxCollider2D>();

        for (int i = 0; i < sprites.Count; i++) // ó���� ��ųʸ��� Ÿ���̶� �׸� �־�
        {
            showType.Add((LineType)i + 1, sprites[i]);
        }
    }

    private void Update()
    {
        if (enemyTypes.Count == 0 && !_isDead) // ���� Ÿ���� ������ �� �������ϱ� ���ϰ���
        {
            Die();
        }
    }

    public void PlayerDraw(LineType attack)
    {
        if (enemyTypes.Count > 0 || !_isDead)
        {
            if (enemyTypes[0] == attack) // ��ųʸ� Ÿ���̶� ù��°���� Ÿ���� ������ �ϳ� ����ž�
            {
                DrawReduce(0);
            }
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

    private IEnumerator Hit()
    {
        _sr.material.SetInt(_isHit, 1);
        yield return new WaitForSeconds(.1f);
        _sr.material.SetInt(_isHit, 0);
    }


    public void Die()
    {
        _isDead = true;
        _hitDecision.enabled = false;
        _emo.enabled = false;
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
        GameManager.Instance.AddEnemy(); // ���� ���ʹ�
    }

    private void FallExp() // Exp ������
    {
        Experience r = PoolManager.Instance.Pop(exp.name) as Experience;
        r.transform.position = transform.position;

        Vector3 offset = Random.insideUnitCircle;

        r.transform.DOJump(transform.position + offset, 2, 1, 0.4f);
    }
}
