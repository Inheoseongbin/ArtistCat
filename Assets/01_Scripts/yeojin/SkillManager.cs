using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SkillSO skillSO;
    [SerializeField] private GameObject includeSkillPanel;
    [SerializeField] private Transform[] panels;

    private void Start()
    {
        // ó�� ���ۿ��� ���ֱ�
        //includeSkillPanel.SetActive(false);

        SkillRandomChoose();
    }

    public void SkillRandomChoose()
    {
        includeSkillPanel.SetActive(true);

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
            randomList.RemoveAt(randIdx);
        }
    }

    private void RandomSkill(int idx, Transform panel)
    {
        TextMeshProUGUI skillName = panel.Find("NOCHANGE/SkillName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillIntrouce = panel.Find("SkillIntroduce").GetComponent<TextMeshProUGUI>();
        Image skillImage = panel.Find("SkillImage").GetComponent<Image>();

        skillName.text = skillSO.list[idx].name;
        skillIntrouce.text = skillSO.list[idx].introduce;
        skillImage.sprite = skillSO.list[idx].image;
    }

    public void DoneChoosing()
    {
        // �� ����� �� ����
        includeSkillPanel.SetActive(false);
    }
}
