using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fence : PoolableMono
{
    public override void Init() {    }

    public static Action bossDie;

    private void Awake()
    {
        bossDie += BossDie;
    }

    public void BossDie()
    {
        PoolManager.Instance.Push(this);
    }
}
