using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SkillSO skillSO;
    [SerializeField] private GameObject includeSkillPanel;
    [SerializeField] private Transform[] panels;

    private void Start()
    {
        includeSkillPanel.SetActive(false);
        SkillRandomChoose(); // 이건 임의
    }

    // 이거 호출해주면 됨
    public void SkillRandomChoose()
    {
        includeSkillPanel.transform.localPosition = new Vector3(0, 1000, 0);
        includeSkillPanel.SetActive(true);
        includeSkillPanel.transform.DOLocalMoveY(-50f, 1f).SetEase(Ease.InOutExpo);

        int idx = skillSO.list.Count; // List 개수 받아오기 1개면 1개
 
        List<int> randomList = new List<int>();
        for (int i = 0; i < idx; i++)
        {
            randomList.Add(i); // 0 ~ 개수만큼
        }

        for (int i = 0; i < 3; i++)
        {
            int randIdx = Random.Range(0, randomList.Count);
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
        // skillSO.list[idx].ID; // 아이디 받아오는 거
        skillName.text = skillSO.list[idx].name;
        skillIntrouce.text = skillSO.list[idx].introduce;
        skillImage.sprite = skillSO.list[idx].image;
    }

    public void ChooseButtonClick(int panelIDX) // 골랐을 때
    {
        TextMeshProUGUI skillName = panels[panelIDX].Find("NameContainer/SkillName").GetComponent<TextMeshProUGUI>();
        print(skillName.text);

        includeSkillPanel.transform.DOLocalMoveY(-1000f,1f).OnComplete(() => includeSkillPanel.SetActive(false));
    }
}
