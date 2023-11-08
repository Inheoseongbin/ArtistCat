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
    public float CurrentPlayTime => currentPlayTime; // ���߿� ���� �ð� ������ ���� �ҷ��� �� ���� �Լ�

    [Header("Setting")]
    [SerializeField] private GameObject settingPanel;
    private bool isSetting = false;

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

        // ����׿�(���� ����)
        if(Input.GetKeyDown(KeyCode.Q))
        {
            AddExperience(20);
        }
    }

    private void TimeShow() // �ð� �����ִ°�
    {
        currentPlayTime += Time.deltaTime;
        timeText.text = $"{Mathf.FloorToInt(currentPlayTime / 60) % 60:00}:{currentPlayTime % 60:00}";
    }

    public void SetDeadUI() // ����� �ҷ���
    {
        gameOverPanel.SetActive(true);
        print("���ӿ����г�ȣ��");
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

    #region ��ų ���� �Լ�

    public void SkillRandomChoose() // �������� �̰� ȣ��
    {
        print("skillRandom");
        includeSkillPanel.SetActive(true);
        includeSkillPanel.transform.localPosition = new Vector3(0, 1000, 0);
        includeSkillPanel.transform.DOLocalMoveY(-55f, 0.7f).SetEase(Ease.InOutQuad)
            .OnComplete(() => Time.timeScale = 0);

        int idx = skillSO.list.Count; // List ���� �޾ƿ��� 1���� 1��

        List<int> randomList = new List<int>();
        for (int i = 0; i < idx; i++)  // 0�� ü�� ȸ��
        {
            if(skillSO.list[i].upgradeLevel == maxLevel) // ���� �� ���� ��
            {
                continue;
            }
            randomList.Add(i); // 0 ~ ������ŭ
        }

        if (randomList.Count < 3) // 3���� ������
        {
            while (randomList.Count < 3)
            {
                randomList.Add(0); // ü�� ȸ���� �־��ֱ�
            }
        }

        for (int i = 0; i < 3; i++)
        {
            int randIdx = Random.Range(0, randomList.Count);

            // ������ �гο� �ش� ���̵� ����
            panelID[i] = skillSO.list[randomList[randIdx]].ID;
            RandomSkill(randomList[randIdx], panels[i]);

            randomList.RemoveAt(randIdx); // �ߺ� ���� �Ϸ��� ���� -> ���߿� ���� ���ɼ� O
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
        // ã���ְ�
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

        // �ش� idx�� �ִ� �� �־��ֱ�
        skillName.text = skillSO.list[idx].name;
        skillIntrouce.text = skillSO.list[idx].introduce;
        skillImage.sprite = skillSO.list[idx].image;
    }

    public void ChooseButtonClick(int pIdx) // ����� ��
    {
        Time.timeScale = 1;
        //print(panelID[pIdx]);
        //print(skillSO.list[panelID[pIdx]].name); // �´���Ȯ��(�ش�id�������̸��߳���)

        includeSkillPanel.transform.DOLocalMoveY(-1000f, 1f)
            .OnComplete(() =>
            {
                includeSkillPanel.SetActive(false);
                Time.timeScale = 1;
            });
        if(skillSO.list[panelID[pIdx]].ID != 0) ++skillSO.list[panelID[pIdx]].upgradeLevel; // 0�� ���׷��̵� ����
        SkillUpgrade(skillSO.list[panelID[pIdx]].ID);
    }

    private void SkillUpgrade(int id)
    {
        Player player = GameManager.Instance.playerTrm.GetComponent<Player>();
        switch(id)
        {
            case 0: // ��
                player.OnHeal(10);
                break;
            case 1: // �ڼ� ���� ����
                player.OnSpeedUp(1f);
                break;
            case 2: // ü�� ȸ��
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
