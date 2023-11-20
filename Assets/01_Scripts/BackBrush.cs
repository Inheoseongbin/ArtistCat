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
        if (currentLineRenderer != null)    
            currentLineRenderer.positionCount = 2;
    }

    private void OnDisable()
    {
        if (currentLineRenderer != null)
            currentLineRenderer.positionCount = 2;
    }
}
