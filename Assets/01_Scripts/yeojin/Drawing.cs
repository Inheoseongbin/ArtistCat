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

    #region �ǵ�� ����
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
        brush.transform.parent = mainCam.transform; // ī�޶� �ڽ����� �ΰ�
        Vector3 pos = mainCam.transform.localPosition; // ī�޶� ���󰡰� �����
        pos.z = 0;
        brush.transform.position = pos;
        currentLineRenderer = brush.GetComponent<LineRenderer>();

        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // ���콺 ������ �����ͼ�
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

        currentType = LineType.NONE; // �⺻ NONE ���� ����

        if (positions.Length <= 4) return; // �׳� Ŭ�� üũ

        WidthHeightCheck(positions);
        VCheck(positions); // V �Ǻ�
        ThunderCheck(positions); // �����Ǻ�

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
            //����
            if (positions[i].x < currentLineRenderer.GetPosition(0).x + _limitValue &&
                positions[i].x > currentLineRenderer.GetPosition(0).x - _limitValue)
            {
                currentType = LineType.LENGTH;
            }
            //����
            else if (positions[i].y < currentLineRenderer.GetPosition(0).y + _limitValue &&
                positions[i].y > currentLineRenderer.GetPosition(0).y - _limitValue)
            {
                currentType = LineType.WIDTH;
            }
        }
    }

    private float maxDisX = 1.0f; // 231102 ���� VCheck �Լ����� ���
    private void VCheck(Vector3[] positions)
    {
        Vector3 startPos = currentLineRenderer.GetPosition(0); // 0��° ������ ��������
        Vector3 endPos = currentLineRenderer.GetPosition(positions.Length - 1); // ������ ������

        // ������ ���� for�� (W, M ����ó�� �׷� �� �����ϰ� ���� ������)
        for (int i = 0; i < positions.Length; i++)
        {
            // ������ �Ǻ�
            if (i > 1 && i < positions.Length - 3 && // ���� ����
                positions[i].y < positions[i - 2].y && positions[i].y < positions[i + 2].y) // �������ٰ� ���ڱ� �ö� (�� ���� ���� ������ ���� ���� ����)
            {
                float firstY = startPos.y; // ó������ �׷��� ���� y�� (button down pos)
                float lastY = endPos.y; // ���������� �׷��� ���� y�� (button up pos)
                float value = Mathf.Abs(startPos.x - endPos.x); // ó�� ���� ������ ���� X ����

                // 1.0 �� ����... ���� ������ ���� ������ ����
                if (firstY - positions[i].y >= 1.0f &&
                    lastY - positions[i].y >= 1.0f && // (ù ��/������ ��)�� ã�Ƴ� �������� ���̰� 1�̻��� ��� (������ ������!!! firstY�� lastY �� ���� pos���� ŭ)
                    value >= maxDisX) // ù ���� �� ���� x�� ����
                {
                    currentType = LineType.V;
                }
            }
            if (i > 1 && i < positions.Length - 3 && // ���� ����
                positions[i].y > positions[i - 2].y && positions[i].y > positions[i + 2].y) // �ö󰡴ٰ� ���ڱ� ���� (�� ���� ���� ������ ���� ���� ŭ)
            {
                float firstY = startPos.y; // ó������ �׷��� ���� y�� (button down pos)
                float lastY = endPos.y; // ���������� �׷��� ���� y�� (button up pos)
                float value = Mathf.Abs(startPos.x - endPos.x); // ó�� ���� ������ ���� X ����

                if (positions[i].y - firstY >= 1.0f &&
                    positions[i].y - lastY >= 1.0f &&
                    value >= maxDisX)
                {
                    currentType = LineType.REVERSEV;
                }
            }
        }
    }

    private void ThunderCheck(Vector3[] positions) // ��..������
    {
        int cnt = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            if (i > 1 && i < positions.Length - 3 && // ���� ����
              positions[i].x < positions[i - 2].x && positions[i].x < positions[i + 2].x) // < �̷� ����϶�
            {
                cnt++;
            }
            else if (i > 1 && i < positions.Length - 3 && // ���� ����
              positions[i].x > positions[i - 2].x && positions[i].x > positions[i + 2].x) // > �̰� �̷� ���
            {
                cnt++;
            }
        }

        if (cnt >= 2 && cnt <= 4) // < > �̷� ����� �� �� �̻�, �� �� ������ ��� ���� ����̶�� ����.
        {
            currentType = LineType.THUNDER;
        }
    }
}