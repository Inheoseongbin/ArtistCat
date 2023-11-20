using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : PoolableMono
{
    public static Action destroyBullet;
    private SpriteRenderer sp;

    public List<Sprite> sprites;

    public override void Init()
    {
        transform.Rotate(Vector3.zero);
        int r = Random.Range(0, sprites.Count);
        sp.sprite = sprites[r];
    }

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
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
