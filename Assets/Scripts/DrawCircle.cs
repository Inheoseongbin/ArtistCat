using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public enum LineType
    {
        NONE,
        WIDTH,
        LENGTH
    }
    LineType currentType;

    public Camera m_camera;

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
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos != lastPos)
            {
                AddPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
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

        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

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

    public void Circle() // 나중에 꼭 도전하겠어 ,,,,,,,,,,,,,,,,,,,,,,,,,
	{
        Vector3[] positions = new Vector3[currentLineRenderer.positionCount];

        currentLineRenderer.GetPositions(positions);

        Vector3 longPos = positions[0];
        Vector3 startPos = positions[0];

        foreach (var p in positions)
        {
            if (Vector3.SqrMagnitude(startPos - p) > Vector3.SqrMagnitude(startPos - longPos))
            {
                longPos = p;
            }
        }

        Vector3 center = new Vector3((startPos.x + longPos.x) / 2, (startPos.y + longPos.y) / 2);
        float radius = Vector3.Distance(startPos, center);
        float errorRange = 0.3f;
        int count = 0;
        foreach (var p in positions)
        {
            float dis = Vector3.Distance(p, center);
            if (dis < radius + errorRange && dis > radius - errorRange)
                count++;
        }

        if (count > currentLineRenderer.positionCount / 2)
            Debug.Log("Circle");
    }
}
