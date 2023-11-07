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
        includeSkillPanel.SetActive(false);
        panelID = new int[3];
        currentPlayTime = 0f;
    }

    private void Update()
    {
        TimeShow();
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
        for (int i = 0; i < idx; i++)
        {
            randomList.Add(i); // 0 ~ ������ŭ
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

    private void RandomSkill(int idx, Transform panel)
    {
        // ã���ְ�
        TextMeshProUGUI skillName = panel.Find("NameContainer/SkillName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillIntrouce = panel.Find("SkillIntroduce").GetComponent<TextMeshProUGUI>();
        Image skillImage = panel.Find("SkillImage").GetComponent<Image>();

        // �ش� idx�� �ִ� �� �־��ֱ�
        skillName.text = skillSO.list[idx].name;
        skillIntrouce.text = skillSO.list[idx].introduce;
        skillImage.sprite = skillSO.list[idx].image;
    }

    public void ChooseButtonClick(int pIdx) // ����� ��
    {
        Time.timeScale = 1;
        print(panelID[pIdx]);
        print(skillSO.list[panelID[pIdx]].name); // �´���Ȯ��(�ش�id�������̸��߳���)

        includeSkillPanel.transform.DOLocalMoveY(-1000f, 1f)
            .OnComplete(() =>
            {
                includeSkillPanel.SetActive(false);
                Time.timeScale = 1;
            });
        returnID(skillSO.list[panelID[pIdx]].ID);
    }

    public int returnID(int id) // �ƾƵ� ���� (�̰� ����Ͽ� � ���������� �˱�)
    {
        return id;
    }
    
    #endregion
}
