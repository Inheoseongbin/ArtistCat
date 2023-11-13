using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Experience : PoolableMono
{
	public int expNum;
	private bool isMagnet = false;
	private bool isSelected = false;
	public bool IsSelected => isSelected;
		
    public override void Init()
	{
		isMagnet = false;
		isSelected = false;
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
			Level.Instance.AddExperience(expNum);
			PoolManager.Instance.Push(this);
			isSelected = true;
		}

		if(collision.CompareTag("Magnet"))
		{
			isMagnet = true;
		}
	}
}
