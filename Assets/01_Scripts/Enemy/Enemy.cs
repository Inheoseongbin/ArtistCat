using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PoolableMono
{
	[Header("적 설정")]
	public List<LineType> enemyTypes;
	public int typeCount;
	private int count;

	[Header("UI 설정")]
	public List<Sprite> sprites = new List<Sprite>();
	public GameObject imageParent;
	public GameObject img;

	private Dictionary<LineType, Sprite> showType = new Dictionary<LineType, Sprite>();
	private List<GameObject> typeList = new List<GameObject>();
	private Sprite sprite;
	

	public override void Init()
	{
		count = typeCount;
		for (int i = 0; i < count; i++)
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
		for (int i = 0; i < sprites.Count; i++)
		{
			showType.Add((LineType)i + 1, sprites[i]);
		}
		//Init();
	}

	private void Update()
	{
		if(enemyTypes.Count == 0)
		{
			Die();
		}
	}

	public void PlayerDraw(LineType attack)
	{
		if (enemyTypes[0] == attack)
		{
			enemyTypes.RemoveAt(0);
			Destroy(typeList[0]);
			typeList.RemoveAt(0);
		}
	}

	public void Die()
	{
		PoolManager.Instance.Push(this);
		print("죽음");
	}

}
