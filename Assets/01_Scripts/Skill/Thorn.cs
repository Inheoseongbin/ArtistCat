using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : PoolableMono
{
	[SerializeField] private int liveTime;

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

	public IEnumerator Live()
	{
		yield return new WaitForSeconds(liveTime);
		PoolManager.Instance.Push(this);
	}
}
