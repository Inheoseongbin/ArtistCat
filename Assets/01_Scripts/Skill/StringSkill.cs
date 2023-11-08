using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringSkill : PoolableMono
{
	[SerializeField] private float speed = 10;
	[SerializeField] private int liveTime = 4;
	[SerializeField] private int coolTime = 5;

	private float xDir;
	private float yDir;

	public override void Init()
	{
		StartCoroutine(TimeToLive(liveTime));
		xDir = Random.Range(-1.0f, 1.0f);
		yDir = Random.Range(-1.0f, 1.0f);
	}

	void Start()
	{
		Init();
	}

	void Update()
	{
		Vector2 dir = new Vector2(xDir, yDir).normalized; // 방향
		transform.Translate(dir * Time.deltaTime * speed, Space.World);
		transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * speed * speed);

		// 카메라 안에
		Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
		if (position.x < 0f)
		{
			position.x = 0f;
			xDir = Random.Range(0.3f, 1.0f);
		}
		if (position.y < 0f)
		{
			position.y = 0f;
			yDir = Random.Range(0.3f, 1.0f);
		}
		if (position.x > 1f)
		{
			position.x = 1f;
			xDir = Random.Range(-1.0f, -0.3f);
		}
		if (position.y > 1f)
		{
			position.y = 1f;
			yDir = Random.Range(-1.0f, -0.3f);
		}
		transform.position = Camera.main.ViewportToWorldPoint(position);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Enemy"))
		{
			Enemy e = collision.GetComponent<Enemy>();
			e.DrawReduce(0);
		}
	}

	IEnumerator TimeToLive(int time)
	{
		yield return new WaitForSeconds(time);
		PoolManager.Instance.Push(this);
	}
}
