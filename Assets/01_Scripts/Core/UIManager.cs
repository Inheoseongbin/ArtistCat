using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("레벨업 UI")]
    [SerializeField] private ExperienceBar expBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("스킬 UI")]
    [SerializeField] private GameObject includeSkillPanel;
    [SerializeField] private Transform[] panels;
    private int[] panelID;
    private bool isSkillChooseOn;
    public bool IsSkillChooseOn => isSkillChooseOn;

    [Header("게임오버 UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI killEnemyText;

    [Header("타이머 UI")]
    [SerializeField] private TextMeshProUGUI timeText;
    private string keyName = "BestScore";

    private float bestTime = 0f;

    [Header("설정 UI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    private bool isSetting = false;
    public bool IsSetting => isSetting;


    public void OnBgmSliderValueChanged()
    {
        float bgmVolume = bgmSlider.value;

        SoundManager.Instance.SetBGMVolume(bgmVolume);
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
    }

    public void OnEffectSliderValueChanged()
    {
        float effectVolume = effectSlider.value;

        SoundManager.Instance.SetEffectVolume(effectVolume);
        PlayerPrefs.SetFloat("EffectVolume", effectVolume);
    }

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
        PlayerPrefs.GetFloat("BGMVolume", 0.1f);
        PlayerPrefs.GetFloat("EffectVolume", 0.4f);

        isSkillChooseOn = false;
        isSetting = false;

        settingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        includeSkillPanel.SetActive(false);

        gameOverPanel.transform.localScale = new Vector3(0, 0, 0);

        panelID = new int[3];

        expBar.UpdateExpBar(0, 0);
        levelText.text = $"Level : {1}";

        bestTime = PlayerPrefs.GetFloat(keyName, float.MinValue);
    }
    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return; // 게임오버일 경우 리턴
        
        if(Input.GetKeyDown(KeyCode.Escape)) // 설정 눌렀을 경우
        {
            isSetting = !isSetting;
            Time.timeScale = isSetting ? 0 : 1;
            settingPanel.SetActive(isSetting);
        }

        timeText.text = $"{Mathf.FloorToInt(GameManager.Instance.CurrentPlayTime / 60) % 60:00}" +
            $":{GameManager.Instance.CurrentPlayTime % 60:00}";
    }

    // 게임 오버 관련
    public void SetDeadUI() // 사망시 불러옴
    {
        gameOverPanel.transform.DOScale(1, 0.8f).OnComplete(() => Time.timeScale = 0);
        gameOverPanel.SetActive(true);
        
        float t = PlayerPrefs.GetFloat(keyName);
        float endTime = GameManager.Instance.EndTime;

        if (t < endTime) // 더 오래 버텼을 경우 최고기록 갱신 // 이것도 나중에 게임 매니저로 옮기고
        {
            PlayerPrefs.SetFloat(keyName, endTime);
            bestTime = endTime;
        }

        currentTimeText.text = $"{Mathf.FloorToInt(endTime / 60) % 60:00}:{endTime % 60:00}";
        bestTimeText.text = $"최고기록 {Mathf.FloorToInt(bestTime / 60) % 60:00}:{bestTime % 60:00}";
        killEnemyText.text = $"처치한 적 {GameManager.Instance.EnemyKill}마리";
    }
    
    // 버튼 (다시시작&처음으로)
    public void RestartSceneBtn()
    {
        // 재시작 눌렀으면 enemy 모두 삭제 시키고
        Time.timeScale = 1;
        gameOverPanel.transform.DOScale(0, 0.8f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }
    public void GoBackToFirstSceneBtn()
    {
        // 처음으로 버튼
    }

    // Level UP
    public void UpdateExp(int experience, int levelUp, int level)
    {
        expBar.UpdateExpBar(experience, levelUp);
        levelText.text = $"Level : {level}";
    }

    public void CheckingCanLevelUp()
    {
        if (!isSkillChooseOn) SkillRandomChoose();
        else StartCoroutine(SequentialSkillRandomChoose());
    }

    // 아래는 다 스킬
    private IEnumerator SequentialSkillRandomChoose()
    {
        print("기다리는중");
        yield return new WaitUntil(() => !isSkillChooseOn);
        print("기다림끝!");
        SkillRandomChoose();
    }
    public void SkillRandomChoose() // 레벨업시 이거 호출
    {
        isSkillChooseOn = true;
        includeSkillPanel.SetActive(true);
        includeSkillPanel.transform.localPosition = new Vector3(0, 1000, 0);
        includeSkillPanel.transform.DOLocalMoveY(-55f, 0.7f).SetEase(Ease.InOutQuad)
            .OnComplete(() => Time.timeScale = 0);

        SkillManager.Instance.SkillRandomChoose();
    }
    private void CheckUpgradeBox(int idx, Transform panel)
    {
        for (int i = 0; i < 5; i++)
        {
            Transform upgradeImage = panel.Find($"UpgradeContainer/CheckContain_{i + 1}");
            GameObject checkImg = upgradeImage.Find("Check").gameObject;
            checkImg.SetActive(false);
        }

        int upgradeLevel = SkillManager.Instance.CheckSkillUpgradeLevel(idx);
        
        for (int i = 0; i < upgradeLevel; i++)
        {
            // 여기는 애니메이션 재생 안함
            Transform upgradeImage = panel.Find($"UpgradeContainer/CheckContain_{i + 1}");
            GameObject checkImg = upgradeImage.Find("Check").gameObject;
            checkImg.SetActive(true);
            checkImg.GetComponent<Image>().color = Color.green; // 그린으로 통일
            checkImg.GetComponent<Animator>().enabled = false;
        }

        Transform image = panel.Find($"UpgradeContainer/CheckContain_{upgradeLevel + 1}");
        GameObject check = image.Find("Check").gameObject;
        check.SetActive(true);
        check.GetComponent<Animator>().enabled = true;
    }
    public void RandomSkill(int iPanelID, int idx, int iPanel)
    {
        panelID[iPanel] = iPanelID;

        Transform panel = panels[iPanel];
        // 찾아주고
        TextMeshProUGUI skillName = panel.Find("NameContainer/SkillName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillIntrouce = panel.Find("SkillIntroduce").GetComponent<TextMeshProUGUI>();
        Image skillImage = panel.Find("SkillImage").GetComponent<Image>();

        int id = SkillManager.Instance.CheckCurrentID(idx);
        if (id != 0)
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
            for (int i = 0; i < 5; i++)
            {
                GameObject upgradeImage = panel.Find($"UpgradeContainer/CheckContain_{i + 1}").gameObject;
                upgradeImage.SetActive(false);
            }
        }

        SkillInclude includeData = SkillManager.Instance.ReturnCurrentInfo(idx);
        // 해당 idx에 있는 거 넣어주깅
        skillName.text = includeData.name;
        skillIntrouce.text = includeData.info;
        skillImage.sprite = includeData.image;
    }
    public void ChooseButtonClick(int pIdx) // 골랐을 때
    {
        Time.timeScale = 1;
   
        includeSkillPanel.transform.DOLocalMoveY(-1000f, 1f)
            .OnComplete(() =>
            {
                isSkillChooseOn = false;
                includeSkillPanel.SetActive(false);
            });

        SkillManager.Instance.PressBtnAndUpgrade(panelID[pIdx]);
    }
}