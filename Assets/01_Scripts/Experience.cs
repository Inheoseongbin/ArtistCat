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
    private bool isAddExp = false;
    public bool IsSelected => isSelected;

    private Transform targetPos;

    public override void Init()
    {
        isAddExp = false;
        isMagnet = false;
        isSelected = false;
    }

    private void Update()
    {
        targetPos = GameObject.Find("ExpTarget").transform;

        if (isAddExp)
        {
            if(Vector3.Distance(transform.position, targetPos.position) <= .5f)
                PoolManager.Instance.Push(this);

            transform.position = Vector3.Lerp(transform.position, targetPos.position, 15 * Time.deltaTime);
        }
        
        else if (isMagnet)
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.playerTrm.position, 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAddExp)
        {
            Level.Instance.AddExperience(expNum);
            SoundManager.Instance.PlayCollectExp();
            isAddExp = true;
            isSelected = true;
        }

        if (collision.CompareTag("Magnet"))
        {
            isMagnet = true;
        }
    }
}
