using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _rotateSpeed;
    public float degTheta = 0;

    //개수만큼 나눠주기
    private Transform _center;
    //private float _theta = 0;

    private void Start()
    {
        _center = transform.parent;
    }
    public void FixedUpdate()
    {
        //_theta = degTheta * Mathf.Deg2Rad;
        // 중점 회전을 하기 위한 코드
        // 중점 회전시 각에 따른 좌표 (center.pos.x + radius * Cos(theta) , center.pos.y + radius * Sin(theta))
        //_theta += _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime;
        degTheta += _rotateSpeed * Time.fixedDeltaTime;
        transform.position =
            new Vector2(_center.position.x + _radius * Mathf.Cos(degTheta*Mathf.Deg2Rad)
            , _center.position.y + _radius * Mathf.Sin(degTheta * Mathf.Deg2Rad));
    }
}
