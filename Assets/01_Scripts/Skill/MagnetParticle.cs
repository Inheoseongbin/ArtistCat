using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetParticle : PoolableMono
{
    public override void Init()
    {
        Invoke(nameof(ResetParticle), 1);
    }

    private void Update()
    {
        transform.position = GameManager.Instance.playerTrm.position;   
    }

    void ResetParticle()
    {
        PoolManager.Instance.Push(this);
    }
}
