using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    private Camera mainCam;
    private Transform cam;

    private LineType currentType;
    private Brush brush;
    private LineRenderer currentLineRenderer;
    private Vector2 lastPos;
    private float _limitValue = 1.5f;

    private void Start()
    {
        mainCam = GameManager.Instance.mainCam;
        cam = mainCam.transform;
    }

    #region 건든거 없음
    private void Update()
    {
        Draw();
    }

    void Draw()
    {
		if (GameManager.Instance.IsGameOver || UIManager.Instance.IsSkillChooseOn || UIManager.Instance.IsSetting)
		{
			if (brush != null) PoolManager.Instance.Push(brush);
			currentLineRenderer = null;
			return;
		}
		if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        if (Input.GetMouseButton(0))
        {
            cam = mainCam.transform;
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 camRelative = cam.InverseTransformPoint(mousePos);
            if (camRelative != lastPos && camRelative != null)
            {
                AddPoint(camRelative);
                lastPos = camRelative;
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
        cam = mainCam.transform;
        brush = PoolManager.Instance.Pop("Brush") as Brush;
        brush.transform.parent = mainCam.transform; // 카메라 자식으로 두고
        Vector3 pos = mainCam.transform.localPosition; // 카메라를 따라가게 만들어
        pos.z = 0;
        brush.transform.position = pos;
        currentLineRenderer = brush.GetComponent<LineRenderer>();

        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // 마우스 포지션 가져와서
        Vector2 camRelative = cam.InverseTransformPoint(mousePos); // inverse

        currentLineRenderer.SetPosition(0, camRelative);
        currentLineRenderer.SetPosition(1, camRelative);
    }

    void AddPoint(Vector2 pointPos)
    {
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount++, pointPos);
        //LineCheck();
    }
    
    #endregion

    private void LineCheck()
    {
        Vector3[] positions = new Vector3[currentLineRenderer.positionCount];
        currentLineRenderer.GetPositions(positions);

        currentType = LineType.NONE; // 기본 NONE 으로 설정

        if (positions.Length <= 4) return; // 그냥 클릭 체크

        WidthHeightCheck(positions);
        VCheck(positions); // V 판별
        ThunderCheck(positions); // 번개판별

        Enemy[] enemies = Object.FindObjectsOfType<Enemy>();
        foreach(Enemy e in enemies)
		{
            e.PlayerDraw(currentType);
		}

        Boss[] bosses = Object.FindObjectsOfType<Boss>();
        foreach (Boss b in bosses)
        {
            b.PlayerDraw(currentType);
        }

        UIManager.Instance.CurrentImage(currentType);

        //Debug.Log(currentType);
    }

    private void WidthHeightCheck(Vector3[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
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
        }
    }

    private float maxDisX = 1.0f; // 231102 기준 VCheck 함수에서 사용
    private void VCheck(Vector3[] positions)
    {
        Vector3 startPos = currentLineRenderer.GetPosition(0); // 0번째 포지션 가져오깅
        Vector3 endPos = currentLineRenderer.GetPosition(positions.Length - 1); // 마지막 포지션

        // 브이자 관련 for문 (W, M 예외처리 그런 거 제외하고 나름 괜찮음)
        for (int i = 0; i < positions.Length; i++)
        {
            // 브이자 판별
            if (i > 1 && i < positions.Length - 3 && // 예외 판정
                positions[i].y < positions[i - 2].y && positions[i].y < positions[i + 2].y) // 내려가다가 갑자기 올라감 (전 점과 다음 점보다 현재 점이 작음)
            {
                float firstY = startPos.y; // 처음으로 그려진 벡터 y값 (button down pos)
                float lastY = endPos.y; // 마지막으로 그려진 벡터 y값 (button up pos)
                float value = Mathf.Abs(startPos.x - endPos.x); // 처음 값과 마지막 값의 X 차이

                // 1.0 걍 대충... 많이 나오는 차이 가져옴 ㅇㅇ
                if (firstY - positions[i].y >= 1.0f &&
                    lastY - positions[i].y >= 1.0f && // (첫 점/마지막 점)과 찾아낸 꼭짓점의 차이가 1이상일 경우 (어차피 무조건!!! firstY랑 lastY 가 현재 pos보다 큼)
                    value >= maxDisX) // 첫 점과 끝 점의 x값 차이
                {
                    currentType = LineType.V;
                }
            }
            if (i > 1 && i < positions.Length - 3 && // 예외 판정
                positions[i].y > positions[i - 2].y && positions[i].y > positions[i + 2].y) // 올라가다가 갑자기 내려 (전 점과 다음 점보다 현재 점이 큼)
            {
                float firstY = startPos.y; // 처음으로 그려진 벡터 y값 (button down pos)
                float lastY = endPos.y; // 마지막으로 그려진 벡터 y값 (button up pos)
                float value = Mathf.Abs(startPos.x - endPos.x); // 처음 값과 마지막 값의 X 차이

                if (positions[i].y - firstY >= 1.0f &&
                    positions[i].y - lastY >= 1.0f &&
                    value >= maxDisX)
                {
                    currentType = LineType.REVERSEV;
                }
            }
        }
    }

    private void ThunderCheck(Vector3[] positions) // 넘..억진가
    {
        int cnt = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            if (i > 1 && i < positions.Length - 3 && // 예외 판정
              positions[i].x < positions[i - 2].x && positions[i].x < positions[i + 2].x) // < 이런 모양일때
            {
                cnt++;
            }
            else if (i > 1 && i < positions.Length - 3 && // 예외 판정
              positions[i].x > positions[i - 2].x && positions[i].x > positions[i + 2].x) // > 이건 이런 모양
            {
                cnt++;
            }
        }

        if (cnt >= 2 && cnt <= 4) // < > 이런 모양이 두 개 이상, 네 개 이하일 경우 번개 모양이라고 가정.
        {
            currentType = LineType.THUNDER;
        }
    }
}