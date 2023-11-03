using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PoolableMono
{
	public List<LineType> enemyTypes;
	public List<Image> show;
	public int typeCount;
	private int count;

	public override void Init()
	{
		count = typeCount;
		for (int i = 0; i < count; i++)
		{
			int r = Random.Range(1, (int)LineType.END);
			enemyTypes.Add((LineType)r);
		}
	}

	private void Awake()
	{
		Init();
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
		}
	}

	public void Die()
	{
		PoolManager.Instance.Push(this);
		print("Á×À½");
	}

}
