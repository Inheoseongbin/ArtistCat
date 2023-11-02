using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practice : MonoBehaviour
{
    public enum LineType
    {
        NONE,
        WIDTH,
        LENGTH,
        V
    }
    LineType currentType;

    public Camera m_camera;
    private bool isFirstUp = false;

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


        //�׳� Ŭ�� üũ
        if (positions.Length <= 4)
        {
            currentType = LineType.NONE;
            return;
        }

        bool isV = false; // ���� üũ ����
        for (int i = 0; i < positions.Length; i++)
        {
            // ������ �Ǻ�
            if (i > 1 && i < positions.Length - 3 && // ���� ����
                positions[i].y < positions[i - 2].y && positions[i].y < positions[i + 2].y) // �������ٰ� ���ڱ� �ö� (�� ���� ���� ������ ���� ���� ����)
            {
                //print(positions[i].y);
                float firstY = currentLineRenderer.GetPosition(0).y; // ó������ �׷��� ���� y�� (button down pos)
                float lastY = currentLineRenderer.GetPosition(positions.Length - 1).y; // ���������� �׷��� ���� y�� (button up pos)

                float value = Mathf.Abs(currentLineRenderer.GetPosition(0).x - currentLineRenderer.GetPosition(positions.Length - 1).x); // ó�� ���� ������ ���� X ����

                if (firstY - positions[i].y >= 1.0f &&
                    lastY - positions[i].y >= 1.0f && // (ù ��/������ ��)�� ã�Ƴ� �������� ���̰� 1�̻��� ��� (������ ������!!! firstY�� lastY �� ���� pos���� ŭ)
                    value >= 0.7f) // ù ���� �� ���� x�� ���̰� 0.7 �̻��� ���
                {
                    isV = true;
                } 
            }
        }

        if (isV)
        {
            currentType = LineType.V; // ����!
        }
        else
        {
            currentType = LineType.NONE;
        }

        Debug.Log(currentType);
    }
}