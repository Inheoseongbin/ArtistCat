using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    [SerializeField] private SkillSO skillSO;
    private int maxLevel = 5;

    private void Awake()
    {
        if (Instance != null) print("스킬매니저 instance 오류");
        Instance = this;
    }

    private void Start()
    {
        skillSO.ResetUpgrade(); // level reset
    }

    public void SkillRandomChoose()
    {
        int idx = skillSO.list.Count; // List 개수 받아오기 1개면 1개

        List<int> randomList = new List<int>();

        for (int i = 0; i < idx; i++)  // 0은 체력 회복
        {
            if (skillSO.list[i].upgradeLevel == maxLevel) // 레벨 다 됐을 때는 넣지 말긔
            {
                continue;
            }
            randomList.Add(i); // 0 ~ 개수만큼
        }

        if (randomList.Count < 3) // 3보다 작으면
        {
            while (randomList.Count < 3)
            {
                randomList.Add(0); // 체력 회복만 넣어주기 (레벨 개념 없음)
            }
        }

        for (int i = 0; i < 3; i++)
        {
            int randIdx = Random.Range(0, randomList.Count);

            // 각각의 패널에 해당 아이디 저장
            UIManager.Instance.RandomSkill(skillSO.list[randomList[randIdx]].ID, randomList[randIdx], i);

            randomList.RemoveAt(randIdx); // 중복 없게 하려고 삭제 -> 나중에 변경 가능성 O
        }
    }

    public void PressBtnAndUpgrade(int idx)
    {
        if (skillSO.list[idx].ID != 0) ++skillSO.list[idx].upgradeLevel; // 0은 업그레이드 안함
        SkillUpgrade(skillSO.list[idx].ID);
    }

    private void SkillUpgrade(int id)
    {
        Player player = GameManager.Instance.playerTrm.GetComponent<Player>();
        switch (id)
        {
            case 0: // 힐
                player.OnHeal(30);
                Heal healParticle = PoolManager.Instance.Pop("HealParticle") as Heal;
                healParticle.transform.position = GameManager.Instance.playerTrm.position;
                break;
            case 1: // speed
                player.OnSpeedUp(1f);
                break;
            case 2: // 마그넷
                player.OnMagnetUpgrade(1);
                MagnetParticle mParticle = PoolManager.Instance.Pop("MagnetParticle") as MagnetParticle;
                mParticle.transform.position = GameManager.Instance.playerTrm.position;
                break;
            case 3: // 털실 호출
                player.OnYarnSpawn(skillSO.list[id].upgradeLevel);
                break;
            case 4: // 물고기 호출
                player.OnFishSpawn(skillSO.list[id].upgradeLevel);
                break;
            case 5: // 똥 던지기
                player.OnPoopSpawn(skillSO.list[id].upgradeLevel);
                break;
            case 6: // 스크래치
                player.OnScratchOn(skillSO.list[id].upgradeLevel);
                break;
            case 7: // 부메랑
                player.OnBoomerangOn(skillSO.list[id].upgradeLevel);
                break;
            default:
                break;
        }
    }

    public int CheckSkillUpgradeLevel(int idx)
    {
        return skillSO.list[idx].upgradeLevel;
    }

    public int CheckCurrentID(int idx)
    {
        return skillSO.list[idx].ID;
    }

    public SkillInclude ReturnCurrentInfo(int idx)
    {
        string name = skillSO.list[idx].name;
        string info = skillSO.list[idx].introduce;
        Sprite sr = skillSO.list[idx].image;

        SkillInclude includeData = new SkillInclude
        {
            name = name,
            info = info,
            image = sr
        };

        return includeData;
    }
}
