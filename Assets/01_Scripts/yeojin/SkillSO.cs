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
    public string introduce;
}

[CreateAssetMenu(menuName ="SO/Skill")]
public class SkillSO : ScriptableObject
{
    public List<SkillClass> list;
}
