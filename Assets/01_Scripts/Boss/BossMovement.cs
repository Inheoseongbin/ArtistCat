using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossMain
{
    [SerializeField] private Transform _playerTr;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    //private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();

        _playerTr = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start()
    {
        _bossValue.isSkill = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        print(_bossValue.isSkill);
    }

    public void OnMove()
    {
        float dis = Vector2.Distance(transform.position, _playerTr.position);

        if (dis > _distance && !_bossValue.isSkill)
            transform.position = Vector2.MoveTowards(transform.position, _playerTr.position, _speed * Time.deltaTime);
    }
}
