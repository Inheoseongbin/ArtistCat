using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("Skill Manager")]
    [SerializeField] private SkillSO skillSO;
    [SerializeField] private GameObject includeSkillPanel;
    [SerializeField] private Transform[] panels;
    [SerializeField] private int[] panelID;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timeText;
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
        includeSkillPanel.SetActive(false);
        panelID = new int[3];
        currentPlayTime = 0f;
    }

    private void Update()
    {
        TimeShow();
    }

    private void TimeShow() // 시간 보여주는거
    {
        currentPlayTime += Time.deltaTime;
        timeText.text = $"{Mathf.FloorToInt(currentPlayTime / 60) % 60:00}:{currentPlayTime % 60:00}";
    }

    public void SetDeadUI() // 사망시 불러옴
    {
        gameOverPanel.SetActive(true);
        print("게임오버패널호출");
    }

    #region 스킬 관련 함수
    
    public void SkillRandomChoose() // 레벨업시 이거 호출
    {
        print("skillRandom");
        includeSkillPanel.SetActive(true);
        includeSkillPanel.transform.localPosition = new Vector3(0, 1000, 0);
        includeSkillPanel.transform.DOLocalMoveY(-55f, 0.7f).SetEase(Ease.InOutQuad)
            .OnComplete(() => Time.timeScale = 0);

        int idx = skillSO.list.Count; // List 개수 받아오기 1개면 1개

        List<int> randomList = new List<int>();
        for (int i = 0; i < idx; i++)
        {
            randomList.Add(i); // 0 ~ 개수만큼
        }

        for (int i = 0; i < 3; i++)
        {
            int randIdx = Random.Range(0, randomList.Count);

            // 각각의 패널에 해당 아이디 저장
            panelID[i] = skillSO.list[randomList[randIdx]].ID;
            RandomSkill(randomList[randIdx], panels[i]);

            randomList.RemoveAt(randIdx); // 중복 없게 하려고 삭제 -> 나중에 변경 가능성 O
        }
    }

    private void RandomSkill(int idx, Transform panel)
    {
        // 찾아주고
        TextMeshProUGUI skillName = panel.Find("NameContainer/SkillName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillIntrouce = panel.Find("SkillIntroduce").GetComponent<TextMeshProUGUI>();
        Image skillImage = panel.Find("SkillImage").GetComponent<Image>();

        // 해당 idx에 있는 거 넣어주깅
        skillName.text = skillSO.list[idx].name;
        skillIntrouce.text = skillSO.list[idx].introduce;
        skillImage.sprite = skillSO.list[idx].image;
    }

    public void ChooseButtonClick(int pIdx) // 골랐을 때
    {
        Time.timeScale = 1;
        print(panelID[pIdx]);
        print(skillSO.list[panelID[pIdx]].name); // 맞는지확인(해당id아이템이름잘나옴)

        includeSkillPanel.transform.DOLocalMoveY(-1000f, 1f)
            .OnComplete(() =>
            {
                includeSkillPanel.SetActive(false);
                Time.timeScale = 1;
            });
        returnID(skillSO.list[panelID[pIdx]].ID);
    }

    public int returnID(int id) // 아아디 리턴 (이거 사용하여 어떤 아이템인지 알기)
    {
        return id;
    }
    
    #endregion
}
