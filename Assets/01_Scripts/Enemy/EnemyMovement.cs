using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private float speed = 1f;
	[SerializeField] private float range = 2f;
	private Transform player;

	private void Awake()
	{
		player = GameManager.Instance.playerTrm;
	}

    private void Update()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis > range)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
