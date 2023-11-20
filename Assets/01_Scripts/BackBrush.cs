using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBrush : MonoBehaviour
{
    private LineRenderer currentLineRenderer;

    public static Action eraseBBrush;

    private void Awake()
    {
        currentLineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        eraseBBrush += ResetBrush;
    }

    public void ResetBrush()
    {
        currentLineRenderer.positionCount = 2;
        //currentLineRenderer.SetPosition(0, Vector3.zero);
        //currentLineRenderer.SetPosition(1, Vector3.zero);
    }
}
