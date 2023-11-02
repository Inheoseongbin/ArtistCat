using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public enum LineType
    {
        NONE,
        WIDTH,
        LENGTH
    }
    LineType currentType;

    Brush brush;
    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    private float _limitValue = 1.5f;

    private void Update()
    {
        Draw();
    }

    void Draw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = GameManager.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos != lastPos)
            {
                AddPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Check and Erase Line
            LineCheck();
            PoolManager.Instance.Push(brush);
        }
        else
        {
            currentLineRenderer = null;
        }
    }

    private void CreateBrush()
    {
        brush = PoolManager.Instance.Pop("Brush") as Brush;
        currentLineRenderer = brush.GetComponent<LineRenderer>();

        Vector2 mousePos = GameManager.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    private void LineCheck()
    {
        Vector3[] positions = new Vector3[currentLineRenderer.positionCount];

        currentLineRenderer.GetPositions(positions);

        for (int i = 0; i < positions.Length; i++)
        {
            //그냥 클릭 체크
            if (positions.Length <= 4)
            {
                currentType = LineType.NONE;
                break;
            }

            //세로
            if (positions[i].x < currentLineRenderer.GetPosition(0).x + _limitValue &&
                positions[i].x > currentLineRenderer.GetPosition(0).x - _limitValue)
            {
                currentType = LineType.LENGTH;
            }
            //가로
            else if (positions[i].y < currentLineRenderer.GetPosition(0).y + _limitValue &&
                positions[i].y > currentLineRenderer.GetPosition(0).y - _limitValue)
            {
                currentType = LineType.WIDTH;
            }
            //잘못 그린거
            else
            {
                currentType = LineType.NONE;
                break;
            }
        }

        Debug.Log(currentType);
    }
}
