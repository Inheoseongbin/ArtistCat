using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PoolableMono
{
    public override void Init()
    {
        Invoke(nameof(HealParticle), 1);
    }

    private void Update()
    {
        transform.position = GameManager.Instance.playerTrm.transform.position;
    }

    void HealParticle()
    {
        PoolManager.Instance.Push(this);
    }
}
