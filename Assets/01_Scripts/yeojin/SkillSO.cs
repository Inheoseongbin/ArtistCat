using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillClass
{
    public int ID; // 스킬 고유 아이디
    public Sprite image;
    public string name;
    [TextArea]
    public string introduce;

    public int upgradeLevel = 0;
}

[CreateAssetMenu(menuName ="SO/Skill")]
public class SkillSO : ScriptableObject
{
    public List<SkillClass> list;

    public void ResetUpgrade()
    {
        // 업그레이드 레벨 초기화
        foreach (SkillClass skill in list)
        {
            skill.upgradeLevel = 0;
        }
    }
}
