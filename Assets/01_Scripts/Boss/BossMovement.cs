using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossMain
{
    [SerializeField] private Transform _playerTr;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    //private Rigidbody2D _rb;

    void Start()
    {
        _playerTr = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
    }

    public void OnMove()
    {
        float dis = Vector2.Distance(transform.position, _playerTr.position);

        if (dis > _distance && !isSkill)
            transform.position = Vector2.MoveTowards(transform.position, _playerTr.position, _speed * Time.deltaTime);
    }
}
