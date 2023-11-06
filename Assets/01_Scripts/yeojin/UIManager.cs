using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverPanel;

    private float currentPlayTime = 0f;
    public float CurrentPlayTime => currentPlayTime; // ���߿� ���� �ð� ������ ���� �ҷ��� �� ���� �Լ�

    private void Awake()
    {
        if(Instance != null)
        {
            print("ui�Ŵ���������������������");
        }
        Instance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        currentPlayTime = 0f;
    }

    private void Update()
    {
        TimeShow();
    }

    private void TimeShow()
    {
        currentPlayTime += Time.deltaTime;
        timeText.text = $"{Mathf.FloorToInt(currentPlayTime / 60) % 60:00}:{currentPlayTime % 60:00}";
    }

    public void SetDeadUI()
    {
        gameOverPanel.SetActive(true);
        print("���ӿ����г�ȣ��");
    }
}
