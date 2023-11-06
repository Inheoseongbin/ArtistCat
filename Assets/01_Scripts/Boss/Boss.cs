using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : PoolableMono
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

	public override void Init()
	{
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
		for (int i = 0; i < sprites.Count; i++) // ó���� ��ųʸ��� Ÿ���̶� �׸� �־�
		{
			showType.Add((LineType)i + 1, sprites[i]);
		}
	}

	private void Update()
	{
		if (enemyTypes.Count == 0) // ���� Ÿ���� ������ �� �������ϱ� ���ϰ���
		{
			Die();
		}
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

	public void Die()
	{
		PoolManager.Instance.Push(this); // ������ Ǯ�� �ֱ�
		FallExp();
	}

	private void FallExp() // Exp ������
	{
		Experience r = PoolManager.Instance.Pop(exp.name) as Experience;
		r.transform.position = transform.position;

		Vector3 offset = Random.insideUnitCircle;

		r.transform.DOJump(transform.position + offset, 2, 1, 0.4f);
	}
}
