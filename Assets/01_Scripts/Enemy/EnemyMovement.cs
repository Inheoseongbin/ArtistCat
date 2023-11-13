using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private float speed = 1f;
	[SerializeField] private float range = 2f;
	private Transform player;
	
	private SpriteRenderer _sr;
	private SpriteRenderer _emo;

	private void Awake()
	{
		player = GameManager.Instance.playerTrm;
		_sr = GameObject.Find("Back").GetComponent<SpriteRenderer>();
		_emo = GameObject.Find("Emo").GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis > range)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

		if(player.transform.position.x < transform.position.x )
		{
			_sr.flipX = true;

			_emo.flipX = true;
		}
		else
		{
			_sr.flipX = false;

			_emo.flipX = false;
		}
    }
}
