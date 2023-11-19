using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Type")]
public class BossType : ScriptableObject
{
    public int Count;

    [Header("��Ÿ��")]
    public float _shootCool;
    public float _dashCool;

    [Header("�Ѿ�")]
    public float _bulletSpeed;

    [Header("�÷�")]
    public Color _BossColor;
}
