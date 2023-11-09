using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : PoolableMono
{
	public override void Init()
	{
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Enemy e = collision.GetComponent<Enemy>();
			e.DrawReduce(0);
		}
	}
}
