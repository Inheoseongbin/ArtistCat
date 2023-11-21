using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolableMono
{
    private SpriteRenderer sp;
    private Rigidbody2D rigid;
    private Vector3 playerDirection;


    public List<Sprite> sprites;
    public float speed = 10;


    public override void Init()
    {
        transform.Rotate(Vector3.zero);
        int r = Random.Range(0, sprites.Count);
        sp.sprite = sprites[r];

        playerDirection = (GameManager.Instance.playerTrm.position - transform.position).normalized;
        rigid.velocity = playerDirection * speed;
    }

    private void OnEnable()
    {
        playerDirection = (GameManager.Instance.playerTrm.position - transform.position).normalized;
        Invoke("BulletPool", 5.0f);
    }

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        transform.position += playerDirection * speed * Time.deltaTime;
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
