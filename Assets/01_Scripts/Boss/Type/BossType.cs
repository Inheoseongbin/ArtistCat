using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Type")]
public class BossType : ScriptableObject
{
    public int Count;

    [Header("쿨타임")]
    public float _shootCool;
    public float _dashCool;

    [Header("총알")]
    public float _bulletSpeed;

    [Header("컬러")]
    public Color _BossColor;

	[Header("애니메이터")]
    public RuntimeAnimatorController animator;
}
