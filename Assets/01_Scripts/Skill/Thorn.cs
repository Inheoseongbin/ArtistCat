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
			ThornParticle thornParticle = PoolManager.Instance.Pop("ThornParticle") as ThornParticle;
			thornParticle.transform.position = transform.position;
            PoolManager.Instance.Push(this);
		}
		if (collision.CompareTag("Boss"))
		{
			Boss b = collision.GetComponent<Boss>();
			b.DrawReduce(0);
            ThornParticle thornParticle = PoolManager.Instance.Pop("ThornParticle") as ThornParticle;
            thornParticle.transform.position = transform.position;
            PoolManager.Instance.Push(this);
		}
	}
}
