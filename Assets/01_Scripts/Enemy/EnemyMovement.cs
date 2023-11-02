using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private float speed = 1f;
	[SerializeField] private GameObject player;
	[SerializeField] private float range = 2f;

    private Vector2 movement;    

	private void Awake()
	{
		player = GameObject.Find("Player");
	}

    private void Update()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis > range)
		{
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            movement = direction;
		}
        else
            movement = transform.position;
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position, speed * Time.deltaTime);
    }	
}
