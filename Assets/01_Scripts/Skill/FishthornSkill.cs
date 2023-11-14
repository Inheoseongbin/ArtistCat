using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishthornSkill : PoolableMono
{
	[SerializeField] private float speed;

	public override void Init()
	{
		StartCoroutine(Throw());
	}

	private void Awake()
	{
		Init();
	}

	IEnumerator Throw()
	{
		yield return new WaitForSeconds(0.1f);
		Vector3 target = transform.position;
		target.x += Random.Range(-5, 5);
		target.y += Random.Range(-5, 5);
		transform.DOJump(target, 2, 1, 0.5f);
		Thorn t = PoolManager.Instance.Pop("Thorn") as Thorn;
		t.transform.position = target;
		yield return new WaitForSeconds(0.6f);
		PoolManager.Instance.Push(this);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Enemy e = collision.GetComponent<Enemy>();
			e.DrawReduce(0);
		}
		if (collision.CompareTag("Boss"))
		{
			Boss b = collision.GetComponent<Boss>();
			b.DrawReduce(0);
		}
	}
}
