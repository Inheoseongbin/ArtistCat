using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : PoolableMono
{
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Init()
    {
        lineRenderer.positionCount = 2;
    }
}
