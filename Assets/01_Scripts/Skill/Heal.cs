using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PoolableMono
{
    public override void Init()
    {
        PoolManager.Instance.Push(this);
    }
}
