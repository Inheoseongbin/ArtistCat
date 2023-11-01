using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShape : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float angleThreshold = 10f; // 각도 유효성 검사를 위한 임계값

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

            // 정확한 값을 설정해야 합니다. 예를 들어, 일정 거리 이내로 시작점과 끝점이 가까우면 닫힌 모양으로 간주할 수 있습니다.
            if (distance < 0.1f)
            {
                print("동");
                return true; // 닫힌 모양 (예: 원)
            }
        }

        return false; // 열린 모양 (예: 직선)
    }

    bool IsRectangle(LineRenderer lineRenderer)
    {
        if (lineRenderer.positionCount < 4)
        {
            return false; // 적어도 4개의 점이 필요
        }

        Vector3 firstPoint = lineRenderer.GetPosition(0);
        Vector3 lastPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        // 시작과 끝점이 같은지 확인
        if (Vector3.Distance(firstPoint, lastPoint) > 0.1f)
        {
            return false;
        }

        // 모든 선의 길이가 유사한지 확인 (직사각형의 성격)
        float lineLengthThreshold = Vector3.Distance(firstPoint, lineRenderer.GetPosition(1));
        for (int i = 1; i < lineRenderer.positionCount - 1; i++)
        {
            float lineLength = Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
            if (Mathf.Abs(lineLength - lineLengthThreshold) > 0.1f)
            {
                return false;
            }
        }

        // 모든 각이 90도에 가까운지 확인
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

        Debug.Log("사");
        return true;
    }
}
