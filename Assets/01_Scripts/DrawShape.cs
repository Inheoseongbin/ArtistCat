using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShape : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float angleThreshold = 10f; // ���� ��ȿ�� �˻縦 ���� �Ӱ谪

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AddPoint(mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
		{
            IsClosedShape(lineRenderer);
            IsRectangle(lineRenderer);
		}
    }

    void AddPoint(Vector3 position)
    {
        int positionCount = lineRenderer.positionCount;
        lineRenderer.positionCount = positionCount + 1;
        lineRenderer.SetPosition(positionCount, position);
    }

    bool IsClosedShape(LineRenderer lineRenderer)
    {
        if (lineRenderer.positionCount >= 2)
        {
            Vector3 startPoint = lineRenderer.GetPosition(0);
            Vector3 endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            float distance = Vector3.Distance(startPoint, endPoint);

            // ��Ȯ�� ���� �����ؾ� �մϴ�. ���� ���, ���� �Ÿ� �̳��� �������� ������ ������ ���� ������� ������ �� �ֽ��ϴ�.
            if (distance < 0.1f)
            {
                print("��");
                return true; // ���� ��� (��: ��)
            }
        }

        return false; // ���� ��� (��: ����)
    }

    bool IsRectangle(LineRenderer lineRenderer)
    {
        if (lineRenderer.positionCount < 4)
        {
            return false; // ��� 4���� ���� �ʿ�
        }

        Vector3 firstPoint = lineRenderer.GetPosition(0);
        Vector3 lastPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        // ���۰� ������ ������ Ȯ��
        if (Vector3.Distance(firstPoint, lastPoint) > 0.1f)
        {
            return false;
        }

        // ��� ���� ���̰� �������� Ȯ�� (���簢���� ����)
        float lineLengthThreshold = Vector3.Distance(firstPoint, lineRenderer.GetPosition(1));
        for (int i = 1; i < lineRenderer.positionCount - 1; i++)
        {
            float lineLength = Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
            if (Mathf.Abs(lineLength - lineLengthThreshold) > 0.1f)
            {
                return false;
            }
        }

        // ��� ���� 90���� ������� Ȯ��
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector3 currentVector = lineRenderer.GetPosition(i + 1) - lineRenderer.GetPosition(i);
            Vector3 nextVector = lineRenderer.GetPosition((i + 2) % lineRenderer.positionCount) - lineRenderer.GetPosition(i + 1);
            float angle = Vector3.Angle(currentVector, nextVector);

            if (Mathf.Abs(angle - 90f) > angleThreshold)
            {
                return false;
            }
        }

        Debug.Log("��");
        return true;
    }
}
