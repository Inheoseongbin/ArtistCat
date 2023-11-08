using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    public override void Init()
    {
        transform.Rotate(Vector3.zero);
    }

    private void OnEnable()
    {
        Invoke("BulletPool", 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            BulletPool();
        }
    }

    private void BulletPool()
    {
        PoolManager.Instance.Push(this);
    }
}
