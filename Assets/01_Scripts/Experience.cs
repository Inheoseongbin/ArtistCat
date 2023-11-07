using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Experience : PoolableMono
{
	public int expNum;
	private float moveSpeed = 4f;
	private bool isMagnet = false;
		
    public override void Init()
	{
		isMagnet = false;
	}

	private void Update()
	{
		if(isMagnet)
			transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.playerTrm.position, 0.05f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			collision.GetComponent<Level>().AddExperience(expNum);
			PoolManager.Instance.Push(this);
		}

		if(collision.CompareTag("Magnet"))
		{
			isMagnet = true;
		}
	}
}
