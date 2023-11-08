using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy : PoolableMono
{
	[Header("적 설정")]
	public List<LineType> enemyTypes;
	public int typeCount;
	private int count;
	public GameObject exp;

	[Header("UI 설정")]
	public List<Sprite> sprites = new List<Sprite>();
	public GameObject imageParent;
	public GameObject img;

	private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
	private List<GameObject> typeList = new List<GameObject>();
	private Sprite sprite;

	[SerializeField] private SpriteRenderer _backSprite;
	private readonly int _dissolve = Shader.PropertyToID("_Dissolve");

	bool _isDead = false;
	private BoxCollider2D _hitDecision;

	public override void Init()
	{
		_hitDecision.enabled = true;
		_isDead = false;

		//쉐이더 값 초기화
		_backSprite.material.SetFloat(_dissolve, 1f);

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
		_hitDecision = GetComponent<BoxCollider2D>();

		for (int i = 0; i < sprites.Count; i++) // 처음에 딕셔너리에 타입이랑 그림 넣어
		{
			showType.Add((LineType)i + 1, sprites[i]);
		}
	}

	private void Update()
	{
		if(enemyTypes.Count == 0 && !_isDead) // 남은 타입이 없으면 다 없앴으니까 죽일거임
		{
			Die();
		}
	}

	public void PlayerDraw(LineType attack)
	{
		if (enemyTypes[0] == attack) // 딕셔너리 타입이랑 첫번째꺼의 타입이 같으면 하나 지울거얌
		{
			DrawReduce(0);
		}
	}

	public void DrawReduce(int id)
	{
		enemyTypes.RemoveAt(id);
		Destroy(typeList[id]);
		typeList.RemoveAt(id);
	}

	public void Die()
	{
		_isDead = true;
        _hitDecision.enabled = false;
        StartCoroutine(DieDissolve(1));
		FallExp();
	}

    private IEnumerator DieDissolve(float time)
    {
        float currentRate;
        float percent = 0;
        float currentTime = 0;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            currentRate = Mathf.Lerp(1, -1, percent);
            _backSprite.material.SetFloat(_dissolve, currentRate);

            yield return null;
        }
        PoolManager.Instance.Push(this); // 죽으면 풀링 넣기
    }

    private void FallExp() // Exp 떨구기
	{
		Experience r = PoolManager.Instance.Pop(exp.name) as Experience;
		r.transform.position = transform.position;

		Vector3 offset = Random.insideUnitCircle;

		r.transform.DOJump(transform.position + offset, 2, 1, 0.4f);
	}
}
