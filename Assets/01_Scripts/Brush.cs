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
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    private void Update()
    {
        if (transform.parent.childCount > 1)
            PoolManager.Instance.Push(this);
    }
}
