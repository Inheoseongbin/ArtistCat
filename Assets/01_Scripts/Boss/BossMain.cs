using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMain : MonoBehaviour
{
    protected BossValue _bossValue;

    protected virtual void Awake()
    {
        _bossValue = transform.GetComponent<BossValue>();
    }
}
