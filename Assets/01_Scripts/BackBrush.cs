using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBrush : MonoBehaviour
{
    private LineRenderer currentLineRenderer;

    private void Awake()
    {
        currentLineRenderer = GetComponent<LineRenderer>();
    }

    private void OnDisable()
    {
        currentLineRenderer.positionCount = 2;
    }
}
