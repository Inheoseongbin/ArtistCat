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
    public float CurrentPlayTime => currentPlayTime; // 나중에 일정 시간 지나면 보스 불러올 때 쓰는 함수

    private void Awake()
    {
        if(Instance != null)
        {
            print("ui매니저에러에러에러에러에");
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
        print("게임오버패널호출");
    }
}
