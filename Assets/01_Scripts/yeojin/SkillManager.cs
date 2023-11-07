using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    [SerializeField] private SkillSO skillSO;
    [SerializeField] private GameObject includeSkillPanel;

    [Header("panel & id")]
    [SerializeField] private Transform[] panels;
    [SerializeField] private int[] panelID;

    private void Awake()
    {
        if (Instance != null) 
        {
            print("SkillManager Error");
        }
        Instance = this;
    }

    private void Start()
    {
        panelID = new int[3];
        includeSkillPanel.SetActive(false);
    }

    // �̰� ȣ�����ָ� ��
    public void SkillRandomChoose()
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

        includeSkillPanel.transform.DOLocalMoveY(-1000f,1f)
            .OnComplete(() =>
            {
                includeSkillPanel.SetActive(false);
                Time.timeScale = 1;
            });
    }
}
