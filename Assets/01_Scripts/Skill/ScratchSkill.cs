using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchSkill : PoolableMono
{
	[SerializeField] private float speed = 1;

	private PlayerMove player;
	private Collider2D col;

	private SpriteRenderer sp;
	private readonly int rate = Shader.PropertyToID("_ShowRate");

	private Vector3 pos;

	public override void Init()
	{
		col.enabled = true;
		pos = transform.parent.position;
/*		if (player.isFilp)
		{
			sp.flipX = false;
			pos.x += 1.2f;
			pos.y -= 0.3f;
		}
		else
		{
			sp.flipX = true;
			pos.x -= 1.2f;
			pos.y -= 0.3f;
		}*/
		transform.position = pos;
		StartCoroutine(Rate(speed));
	}

	private void Awake()
	{
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		transform.parent = player.transform;
		sp = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
		Init();
	}

	private void Update()
	{
		pos = transform.parent.position;
		if (player.isFilp)
		{
			sp.flipX = false;
			pos.x += 1.2f;
			pos.y -= 0.3f;
		}
		else
		{
			sp.flipX = true;
			pos.x -= 1.2f;
			pos.y -= 0.3f;
		}
		transform.position = pos;
	}

	private IEnumerator Rate(float time)
	{
		float currentRate;
		float percent = 0;
		float currentTime = 0;

		while (percent < 1)
		{
			currentTime += Time.deltaTime;
			percent = currentTime / time;
			currentRate = Mathf.Lerp(1, -1, percent);
			sp.material.SetFloat(rate, currentRate);

			yield return null;
		}

		yield return new WaitForSeconds(0.2f);
		col.enabled = false;
		sp.material.SetFloat(rate, 1);
		PoolManager.Instance.Push(this);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Enemy e = collision.GetComponent<Enemy>();
			e.DrawReduce(0);
		}
	}
}
