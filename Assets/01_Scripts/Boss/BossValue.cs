using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossValue : MonoBehaviour
{
    public float _speed;
    public float _DashSpeed;

    public bool _isSkill;
    public bool _isDash;
    public bool _isJump;

    public Vector2 myDir;
    public Vector3 saveTr;
    public Transform _playerTr;
}
