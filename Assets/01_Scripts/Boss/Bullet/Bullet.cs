using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BulletPool();
        }
        else
        {
            Invoke("BulletPool", 10.0f);
        }
    }

    private void BulletPool()
    {
        PoolManager.Instance.Push(this);
    }
}
