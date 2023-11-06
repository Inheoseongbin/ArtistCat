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

    // 이거 호출해주면 됨
    public void SkillRandomChoose()
    {
        print("skillRandom");
        includeSkillPanel.SetActive(true);
        includeSkillPanel.transform.localPosition = new Vector3(0, 1000, 0);
        includeSkillPanel.transform.DOLocalMoveY(-55f, 0.5f).SetEase(Ease.InOutQuad)
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

        includeSkillPanel.transform.DOLocalMoveY(-1000f,1f)
            .OnComplete(() =>
            {
                includeSkillPanel.SetActive(false);
                Time.timeScale = 1;
            });
    }
}
