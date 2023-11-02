using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineType
{
    NONE,
    WIDTH,
    LENGTH,
    V,
    REVERSEV,
    THUNDER
}

public class practice : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

    private LineType currentType;
    private Brush brush;
    private LineRenderer currentLineRenderer;
    private Vector2 lastPos;

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

        currentType = LineType.NONE; // �⺻ NONE ���� ����
        Vector3 startPos = currentLineRenderer.GetPosition(0); // 0��° ������ ��������

        //�׳� Ŭ�� üũ
        if (positions.Length <= 4)
        {
            return;
        }

        bool isV = false; // ���� üũ ����
        bool isReverseV = false; // ������ ���� üũ ����
        
        // ������ ���� for�� (W, M ����ó�� �׷� �� �����ϰ� ���� ������)
        for (int i = 0; i < positions.Length; i++)
        {
            // ������ �Ǻ�
            if (i > 1 && i < positions.Length - 3 && // ���� ����
                positions[i].y < positions[i - 2].y && positions[i].y < positions[i + 2].y) // �������ٰ� ���ڱ� �ö� (�� ���� ���� ������ ���� ���� ����)
            {
                //print(positions[i].y);
                float firstY = startPos.y; // ó������ �׷��� ���� y�� (button down pos)
                float lastY = currentLineRenderer.GetPosition(positions.Length - 1).y; // ���������� �׷��� ���� y�� (button up pos)

                float value = Mathf.Abs(startPos.x - currentLineRenderer.GetPosition(positions.Length - 1).x); // ó�� ���� ������ ���� X ����

                if (firstY - positions[i].y >= 1.0f &&
                    lastY - positions[i].y >= 1.0f && // (ù ��/������ ��)�� ã�Ƴ� �������� ���̰� 1�̻��� ��� (������ ������!!! firstY�� lastY �� ���� pos���� ŭ)
                    value >= 0.7f) // ù ���� �� ���� x�� ���̰� 0.7 �̻��� ���
                {
                    isV = true;
                } 
            }
            if(i > 1 && i < positions.Length - 3 && // ���� ����
                positions[i].y > positions[i - 2].y && positions[i].y > positions[i + 2].y) // �ö󰡴ٰ� ���ڱ� ���� (�� ���� ���� ������ ���� ���� ŭ)
            {
                float firstY = startPos.y; // ó������ �׷��� ���� y�� (button down pos)
                float lastY = currentLineRenderer.GetPosition(positions.Length - 1).y; // ���������� �׷��� ���� y�� (button up pos)

                float value = Mathf.Abs(startPos.x - currentLineRenderer.GetPosition(positions.Length - 1).x); // ó�� ���� ������ ���� X ����

                if (positions[i].y - firstY >= 1.0f &&
                    positions[i].y - lastY >= 1.0f && // (ù ��/������ ��)�� ã�Ƴ� �������� ���̰� 1�̻��� ��� (������ ������!!! firstY�� lastY �� ���� pos���� ����)
                    value >= 0.7f) // ù ���� �� ���� x�� ���̰� 0.7 �̻��� ��� (���� ������ ���� ���X)
                {
                    isReverseV = true;
                }
            }
        }

        // ���� �Ǻ�
        if(currentType == LineType.NONE)
        {
            if(ThunderCheck(positions))
            {
                currentType = LineType.THUNDER;
            }
        }

        if (isV)
        {
            currentType = LineType.V;
        }
        else if(isReverseV)
        {
            currentType = LineType.REVERSEV;
        }

        Debug.Log(currentType);
    }

    private bool ThunderCheck(Vector3[] positions)
    {
        int cnt = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            if (i > 1 && i < positions.Length - 3 && // ���� ����
              positions[i].x < positions[i - 2].x && positions[i].x < positions[i + 2].x && 
              positions[i].y < positions[i + 2].x) // ��� y���� Ŀ���� // �ٵ����⼭����!!!! �Ųٷ� �׸���?? ���� ���̷� �ؾ���
            {
                cnt++;
            }
            else if (i > 1 && i < positions.Length - 3 && // ���� ����
              positions[i].x > positions[i - 2].x && positions[i].x > positions[i + 2].x)
            {
                cnt++;
            }
        }

        return cnt >= 2 && cnt <= 6;
    }
}