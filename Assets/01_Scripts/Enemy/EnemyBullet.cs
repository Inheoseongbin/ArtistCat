using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolableMono
{
	[SerializeField] private float speed = 20;

	public override void Init()
	{

	}


	public void Move(Vector3 dir)
	{
		transform.position += dir.normalized * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			PoolManager.Instance.Push(this);
		}
	}
}
