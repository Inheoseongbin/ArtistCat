using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSkill : PoolableMono
{
	[SerializeField] private float speed = 10;
	[SerializeField] private int lifeTime = 5;
	private Vector3 dir;

	private SpriteRenderer sp;
	private Collider2D col;

	public override void Init()
	{
		speed = 6;
		
		sp.enabled = true;
		col.enabled = true;

		Enemy e = FindAnyObjectByType<Enemy>();
		if(e != null)
		{
			dir = e.transform.position - transform.position;
			dir.Normalize();
		}
		else
		{
			float x = Random.Range(-5, 5);
			float y = Random.Range(-5, 5);

			if (dir == Vector3.zero)
			{
				x = Random.Range(-5, 5);
				y = Random.Range(-5, 5);
			}

			dir = new Vector3(x, y).normalized;
		}
	}

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
		Init();
	}

	private void Update()
	{
		transform.position += dir * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Enemy e = collision.GetComponent<Enemy>();
			e.DrawReduce(0);
		}
	}

	public IEnumerator Poop()
	{
		yield return new WaitForSeconds(lifeTime);
		PoolManager.Instance.Push(this);
	}
}
