using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTr;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
    }

    public  void OnMove()
    {
        Vector3 viewDir = _playerTr.position - transform.position;

        transform.position = Vector2.MoveTowards(transform.position, _playerTr.position,  3);
    }
}
