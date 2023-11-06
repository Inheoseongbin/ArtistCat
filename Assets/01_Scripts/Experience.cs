using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : PoolableMono
{
	public int expNum;
	
	public override void Init()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			PoolManager.Instance.Push(this);
		}
	}
}
