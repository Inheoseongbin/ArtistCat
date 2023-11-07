using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossMain
{
    [SerializeField] private float _distance;

    //private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();

        _bossValue._playerTr = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start()
    {
        _bossValue._isSkill = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
    }

    public void OnMove()
    {
        float dis = Vector2.Distance(transform.position, _bossValue._playerTr.position);

        if (dis > _distance && !_bossValue._isSkill && !_bossValue._isDash)
        {
            _rb.velocity = Vector2.zero;
            transform.position = Vector2.MoveTowards(transform.position, _bossValue._playerTr.position, _bossValue._speed * Time.deltaTime);
        }
    }
}
