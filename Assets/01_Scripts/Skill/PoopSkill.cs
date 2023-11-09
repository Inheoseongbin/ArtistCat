using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSkill : PoolableMono
{
	[SerializeField] private float speed = 10;
	[SerializeField] private int coolTime = 4;
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
		StartCoroutine(Poop(lifeTime, coolTime));
		Init();
	}

	private void Update()
	{
		transform.position += dir * speed * Time.deltaTime;

		Dead();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Enemy e = collision.GetComponent<Enemy>();
			e.DrawReduce(0);
			Disappear();
		}
	}

	private IEnumerator Poop(int life, int cool)
	{
		yield return new WaitForSeconds(life);
		Disappear();
		yield return new WaitForSeconds(cool);
		transform.position = GameManager.Instance.playerTrm.position;
		Init();
		StartCoroutine(Poop(lifeTime, coolTime));
	}

	private void Disappear()
	{
		speed = 0;
		sp.enabled = false;
		col.enabled = false;
	}

	void Dead()
	{
		Transform target = GameManager.Instance.playerTrm;
		Vector3 targetPos = target.position;
		float dir = Vector3.Distance(targetPos, transform.position);
		if (dir > 20f)
			PoolManager.Instance.Push(this);
	}
}
