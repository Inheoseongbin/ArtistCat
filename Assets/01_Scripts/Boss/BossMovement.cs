using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossMain
{
    [SerializeField] private float _distance;

    private SpriteRenderer _sr;
    //private Fence _fence;


    void Start()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        Fence _fence = PoolManager.Instance.Pop("Fence") as Fence;
        _fence.transform.position = transform.position;
        _bossValue._playerTr = GameObject.Find("Player").GetComponent<Transform>();
        _bossValue._isSkill = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        //print(_fence);
    }

    public void OnMove()
    {
        float dis = Vector2.Distance(transform.position, _bossValue._playerTr.position);

        if (dis > _distance && !_bossValue._isSkill && !_bossValue._isDash)
        {
            _rb.velocity = Vector2.zero;
            transform.position = Vector2.MoveTowards(transform.position, _bossValue._playerTr.position, _bossValue._speed * Time.deltaTime);
        }

        if (_bossValue._playerTr.position.x < transform.position.x)
            _sr.flipX = true;
        else
            _sr.flipX = false;
    }
}
