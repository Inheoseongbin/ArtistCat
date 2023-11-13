using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _rotateSpeed;
    public float degTheta = 0;

    //������ŭ �����ֱ�
    private Transform _center;
    //private float _theta = 0;

    private void Start()
    {
        _center = transform.parent;
    }
    public void FixedUpdate()
    {
        //_theta = degTheta * Mathf.Deg2Rad;
        // ���� ȸ���� �ϱ� ���� �ڵ�
        // ���� ȸ���� ���� ���� ��ǥ (center.pos.x + radius * Cos(theta) , center.pos.y + radius * Sin(theta))
        //_theta += _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime;
        degTheta += _rotateSpeed * Time.fixedDeltaTime;
        transform.position =
            new Vector2(_center.position.x + _radius * Mathf.Cos(degTheta*Mathf.Deg2Rad)
            , _center.position.y + _radius * Mathf.Sin(degTheta * Mathf.Deg2Rad));
    }
}
