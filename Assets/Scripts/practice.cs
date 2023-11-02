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


        //그냥 클릭 체크
        if (positions.Length <= 4)
        {
            currentType = LineType.NONE;
            return;
        }

        bool isV = false; // 브이 체크 변수
        for (int i = 0; i < positions.Length; i++)
        {
            // 브이자 판별
            if (i > 1 && i < positions.Length - 3 && // 예외 판정
                positions[i].y < positions[i - 2].y && positions[i].y < positions[i + 2].y) // 내려가다가 갑자기 올라감 (전 점과 다음 점보다 현재 점이 작음)
            {
                //print(positions[i].y);
                float firstY = currentLineRenderer.GetPosition(0).y; // 처음으로 그려진 벡터 y값 (button down pos)
                float lastY = currentLineRenderer.GetPosition(positions.Length - 1).y; // 마지막으로 그려진 벡터 y값 (button up pos)

                float value = Mathf.Abs(currentLineRenderer.GetPosition(0).x - currentLineRenderer.GetPosition(positions.Length - 1).x); // 처음 값과 마지막 값의 X 차이

                if (firstY - positions[i].y >= 1.0f &&
                    lastY - positions[i].y >= 1.0f && // (첫 점/마지막 점)과 찾아낸 꼭짓점의 차이가 1이상일 경우 (어차피 무조건!!! firstY랑 lastY 가 현재 pos보다 큼)
                    value >= 0.7f) // 첫 점과 끝 점의 x값 차이가 0.7 이상일 경우
                {
                    isV = true;
                } 
            }
        }

        if (isV)
        {
            currentType = LineType.V; // 브이!
        }
        else
        {
            currentType = LineType.NONE;
        }

        Debug.Log(currentType);
    }
}