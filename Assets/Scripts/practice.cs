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

        currentType = LineType.NONE; // 기본 NONE 으로 설정
        Vector3 startPos = currentLineRenderer.GetPosition(0); // 0번째 포지션 가져오깅

        //그냥 클릭 체크
        if (positions.Length <= 4)
        {
            return;
        }

        bool isV = false; // 브이 체크 변수
        bool isReverseV = false; // 리버스 브이 체크 변수
        
        // 브이자 관련 for문 (W, M 예외처리 그런 거 제외하고 나름 괜찮음)
        for (int i = 0; i < positions.Length; i++)
        {
            // 브이자 판별
            if (i > 1 && i < positions.Length - 3 && // 예외 판정
                positions[i].y < positions[i - 2].y && positions[i].y < positions[i + 2].y) // 내려가다가 갑자기 올라감 (전 점과 다음 점보다 현재 점이 작음)
            {
                //print(positions[i].y);
                float firstY = startPos.y; // 처음으로 그려진 벡터 y값 (button down pos)
                float lastY = currentLineRenderer.GetPosition(positions.Length - 1).y; // 마지막으로 그려진 벡터 y값 (button up pos)

                float value = Mathf.Abs(startPos.x - currentLineRenderer.GetPosition(positions.Length - 1).x); // 처음 값과 마지막 값의 X 차이

                if (firstY - positions[i].y >= 1.0f &&
                    lastY - positions[i].y >= 1.0f && // (첫 점/마지막 점)과 찾아낸 꼭짓점의 차이가 1이상일 경우 (어차피 무조건!!! firstY랑 lastY 가 현재 pos보다 큼)
                    value >= 0.7f) // 첫 점과 끝 점의 x값 차이가 0.7 이상일 경우
                {
                    isV = true;
                } 
            }
            if(i > 1 && i < positions.Length - 3 && // 예외 판정
                positions[i].y > positions[i - 2].y && positions[i].y > positions[i + 2].y) // 올라가다가 갑자기 내려 (전 점과 다음 점보다 현재 점이 큼)
            {
                float firstY = startPos.y; // 처음으로 그려진 벡터 y값 (button down pos)
                float lastY = currentLineRenderer.GetPosition(positions.Length - 1).y; // 마지막으로 그려진 벡터 y값 (button up pos)

                float value = Mathf.Abs(startPos.x - currentLineRenderer.GetPosition(positions.Length - 1).x); // 처음 값과 마지막 값의 X 차이

                if (positions[i].y - firstY >= 1.0f &&
                    positions[i].y - lastY >= 1.0f && // (첫 점/마지막 점)과 찾아낸 꼭짓점의 차이가 1이상일 경우 (어차피 무조건!!! firstY랑 lastY 가 현재 pos보다 작음)
                    value >= 0.7f) // 첫 점과 끝 점의 x값 차이가 0.7 이상일 경우 (추후 변수로 빼도 상관X)
                {
                    isReverseV = true;
                }
            }
        }

        // 번개 판별
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
            if (i > 1 && i < positions.Length - 3 && // 예외 판정
              positions[i].x < positions[i - 2].x && positions[i].x < positions[i + 2].x && 
              positions[i].y < positions[i + 2].x) // 적어도 y값은 커야함 // 근데여기서문제!!!! 거꾸로 그리면?? 절댓값 차이로 해야함
            {
                cnt++;
            }
            else if (i > 1 && i < positions.Length - 3 && // 예외 판정
              positions[i].x > positions[i - 2].x && positions[i].x > positions[i + 2].x)
            {
                cnt++;
            }
        }

        return cnt >= 2 && cnt <= 6;
    }
}