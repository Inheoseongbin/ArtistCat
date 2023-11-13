using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PoolableMono
{
    public override void Init()
    {
        Invoke(nameof(HealParticle), 1);
    }

    void HealParticle()
    {
        PoolManager.Instance.Push(this);
    }
}
