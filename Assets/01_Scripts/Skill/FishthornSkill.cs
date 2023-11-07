using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishthornSkill : PoolableMono
{
	[SerializeField] private float speed = 10;
	private Vector3 dir;

	public override void Init()
	{
		float x = Random.Range(-5, 5);
		float y = Random.Range(-5, 5);

		if (dir == Vector3.zero)
		{
			x = Random.Range(-5, 5);
			y = Random.Range(-5, 5);
		}

		dir = new Vector3(x, y).normalized;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // È¸Àü
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private void Awake()
	{
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
			PoolManager.Instance.Push(this);
		}
	}
}
