using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono
{
	public List<LineType> enemyTypes;
	public int typeCount;

	public override void Init()
	{
		for (int i = 0; i < typeCount; i++)
		{
			int r = Random.Range(0, 4);
		}
	}

	private void Awake()
	{
		
	}

	private void Update()
	{
		if(enemyTypes.Count == 0)
			Die();
	}

	public void PlayerDraw(LineType attack)
	{
		if (enemyTypes[0] == attack)
		{
			print(attack);
			enemyTypes.RemoveAt(0);
		}
	}

	public void Die()
	{
		print("Á×À½");
	}

}
