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
        SkillRandomChoose(); // �̰� ����
    }

    // �̰� ȣ�����ָ� ��
    public void SkillRandomChoose()
    {
        includeSkillPanel.transform.localPosition = new Vector3(0, 1000, 0);
        includeSkillPanel.SetActive(true);
        includeSkillPanel.transform.DOLocalMoveY(-50f, 1f).SetEase(Ease.InOutExpo);

        int idx = skillSO.list.Count; // List ���� �޾ƿ��� 1���� 1��
 
        List<int> randomList = new List<int>();
        for (int i = 0; i < idx; i++)
        {
            randomList.Add(i); // 0 ~ ������ŭ
        }

        for (int i = 0; i < 3; i++)
        {
            int randIdx = Random.Range(0, randomList.Count);
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
        // skillSO.list[idx].ID; // ���̵� �޾ƿ��� ��
        skillName.text = skillSO.list[idx].name;
        skillIntrouce.text = skillSO.list[idx].introduce;
        skillImage.sprite = skillSO.list[idx].image;
    }

    public void ChooseButtonClick(int panelIDX) // ����� ��
    {
        TextMeshProUGUI skillName = panels[panelIDX].Find("NameContainer/SkillName").GetComponent<TextMeshProUGUI>();
        print(skillName.text);

        includeSkillPanel.transform.DOLocalMoveY(-1000f,1f).OnComplete(() => includeSkillPanel.SetActive(false));
    }
}
