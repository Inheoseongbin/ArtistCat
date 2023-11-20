using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    public static Action destroyBullet;

    public override void Init()
    {
        transform.Rotate(Vector3.zero);
    }

    private void OnEnable()
    {
        destroyBullet += BulletPool;
        Invoke("BulletPool", 4.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            BulletPool();
        }
    }

    public void BulletPool()
    {
        PoolManager.Instance.Push(this);
    }
}
