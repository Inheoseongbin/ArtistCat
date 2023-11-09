using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishthornSkill : PoolableMono
{
	[SerializeField] private float speed;

	private Rigidbody2D rb;
	private Vector3 thornPos;

	public override void Init()
	{
		StartCoroutine(Throw());
	}

	private void Awake()
	{
		Init();
	}

	private void Update()
	{

	}

	IEnumerator Throw()
	{
		yield return new WaitForSeconds(0.1f);
		Vector3 target = transform.position;
		target.x += Random.Range(-4, 4);
		target.y += Random.Range(-4, 4);
		transform.DOJump(target, 2, 1, 0.5f);
		Thorn t = PoolManager.Instance.Pop("Thorn") as Thorn;
		t.transform.position = target;
	}

	void OnTriggerEnter2D(Collider2D col)
	{ 
		if (col.CompareTag("Enemy"))
		{
			print("Qkd");
		}

	}
}
