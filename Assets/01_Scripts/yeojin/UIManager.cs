using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Level")]
    [SerializeField] private ExperienceBar expBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private int level = 1;
    private int experience = 0;

    private int levelUp
    {
        get { return level * 20; }
    }

    [Header("Skill Manager")]
    [SerializeField] private SkillSO skillSO;
    [SerializeField] private GameObject includeSkillPanel;
    [SerializeField] private Transform[] panels;
    private int[] panelID;
    private int maxLevel = 5;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timeText;
    private float currentPlayTime = 0f;
    public float CurrentPlayTime => currentPlayTime; // 나중에 일정 시간 지나면 보스 불러올 때 쓰는 함수

    [Header("Setting")]
    [SerializeField] private GameObject settingPanel;
    private bool isSetting = false;

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
        skillSO.ResetUpgrade();

        settingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        includeSkillPanel.SetActive(false);
        panelID = new int[3];
        currentPlayTime = 0f;

        expBar.UpdateExpBar(experience, levelUp);
        levelText.text = $"Level : {level}";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // setting
        {
            isSetting = !isSetting;
            Time.timeScale = isSetting ? 0 : 1;
            settingPanel.SetActive(isSetting);
        }
        TimeShow();

        // 디버그용(추후 삭제)
        if(Input.GetKeyDown(KeyCode.Q))
        {
            AddExperience(20);
        }
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

    // Level UP
    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        expBar.UpdateExpBar(experience, levelUp);
        levelText.text = $"Level : {level}";
    }

    public void CheckLevelUp()
    {
        if (experience >= levelUp)
        {
            experience -= levelUp;
            level += 1;
            SkillRandomChoose();
        }
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
        for (int i = 0; i < idx; i++)  // 0은 체력 회복
        {
            if(skillSO.list[i].upgradeLevel == maxLevel) // 레벨 다 됐을 때
            {
                continue;
            }
            randomList.Add(i); // 0 ~ 개수만큼
        }

        if (randomList.Count < 3) // 3보다 작으면
        {
            while (randomList.Count < 3)
            {
                randomList.Add(0); // 체력 회복만 넣어주기
            }
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

    private void CheckUpgradeBox(int idx, Transform panel)
    {
        //print($"{idx}Upgrade");
        for (int i = 0; i < 5; i++)
        {
            Transform upgradeImage = panel.Find($"UpgradeContainer/CheckContain_{i + 1}");
            GameObject checkImg = upgradeImage.Find("Check").gameObject;
            checkImg.SetActive(false);
        }
        for (int i = 0; i < skillSO.list[idx].upgradeLevel; i++)
        {
            Transform upgradeImage = panel.Find($"UpgradeContainer/CheckContain_{i + 1}");
            GameObject checkImg = upgradeImage.Find("Check").gameObject;
            checkImg.SetActive(true);
        }
    }

    private void RandomSkill(int idx, Transform panel)
    {
        // 찾아주고
        TextMeshProUGUI skillName = panel.Find("NameContainer/SkillName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillIntrouce = panel.Find("SkillIntroduce").GetComponent<TextMeshProUGUI>();
        Image skillImage = panel.Find("SkillImage").GetComponent<Image>();

        if (skillSO.list[idx].ID != 0)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject upgradeImage = panel.Find($"UpgradeContainer/CheckContain_{i + 1}").gameObject;
                upgradeImage.SetActive(true);
            }
            CheckUpgradeBox(idx, panel);
        }
        else
        {
            //print("0");
            for (int i = 0; i < 5; i++)
            {
                GameObject upgradeImage = panel.Find($"UpgradeContainer/CheckContain_{i + 1}").gameObject;
                upgradeImage.SetActive(false);
            }
        }

        // 해당 idx에 있는 거 넣어주깅
        skillName.text = skillSO.list[idx].name;
        skillIntrouce.text = skillSO.list[idx].introduce;
        skillImage.sprite = skillSO.list[idx].image;
    }

    public void ChooseButtonClick(int pIdx) // 골랐을 때
    {
        Time.timeScale = 1;
        //print(panelID[pIdx]);
        //print(skillSO.list[panelID[pIdx]].name); // 맞는지확인(해당id아이템이름잘나옴)

        includeSkillPanel.transform.DOLocalMoveY(-1000f, 1f)
            .OnComplete(() =>
            {
                includeSkillPanel.SetActive(false);
                Time.timeScale = 1;
            });
        if(skillSO.list[panelID[pIdx]].ID != 0) ++skillSO.list[panelID[pIdx]].upgradeLevel; // 0은 업그레이드 안함
        SkillUpgrade(skillSO.list[panelID[pIdx]].ID);
    }

    private void SkillUpgrade(int id)
    {
        Player player = GameManager.Instance.playerTrm.GetComponent<Player>();
        switch(id)
        {
            case 0: // 힐
                player.OnHeal(10);
                break;
            case 1: // 자석 범위 증가
                player.OnSpeedUp(1f);
                break;
            case 2: // 체력 회복
                player.OnMagnetUpgrade(1);
                break;
            case 3:
                player.OnYarnTrue();
                break;
            case 4:
                player.OnFishTrue();
                break;
            case 5:
                break;
            default:
                break;
        }
    }
    
    #endregion
}
