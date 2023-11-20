using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornParticle : PoolableMono
{
    public override void Init()
    {
        Invoke(nameof(ResetParticle), 1);
    }

    void ResetParticle()
    {
        PoolManager.Instance.Push(this);
    }
}
