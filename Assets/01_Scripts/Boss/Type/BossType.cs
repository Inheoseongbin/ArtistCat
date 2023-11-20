using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/Type")]
public class BossType : ScriptableObject
{
    public int Count;

    [Header("ÄðÅ¸ÀÓ")]
    public float _shootCool;
    public float _dashCool;

    [Header("ÃÑ¾Ë")]
    public float _bulletSpeed;

    [Header("ÄÃ·¯")]
    public Color _BossColor;
}
