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
        if (Instance != null) print("��ų�Ŵ��� instance ����");
        Instance = this;
    }

    private void Start()
    {
        skillSO.ResetUpgrade(); // level reset
    }

    public void SkillRandomChoose()
    {
        int idx = skillSO.list.Count; // List ���� �޾ƿ��� 1���� 1��

        List<int> randomList = new List<int>();

        for (int i = 0; i < idx; i++)  // 0�� ü�� ȸ��
        {
            if (skillSO.list[i].upgradeLevel == maxLevel) // ���� �� ���� ���� ���� ����
            {
                continue;
            }
            randomList.Add(i); // 0 ~ ������ŭ
        }

        if (randomList.Count < 3) // 3���� ������
        {
            while (randomList.Count < 3)
            {
                randomList.Add(0); // ü�� ȸ���� �־��ֱ� (���� ���� ����)
            }
        }

        for (int i = 0; i < 3; i++)
        {
            int randIdx = Random.Range(0, randomList.Count);

            // ������ �гο� �ش� ���̵� ����
            UIManager.Instance.RandomSkill(skillSO.list[randomList[randIdx]].ID, randomList[randIdx], i);

            randomList.RemoveAt(randIdx); // �ߺ� ���� �Ϸ��� ���� -> ���߿� ���� ���ɼ� O
        }
    }

    public void PressBtnAndUpgrade(int idx)
    {
        if (skillSO.list[idx].ID != 0) ++skillSO.list[idx].upgradeLevel; // 0�� ���׷��̵� ����
        SkillUpgrade(skillSO.list[idx].ID);
    }

    private void SkillUpgrade(int id)
    {
        Player player = GameManager.Instance.playerTrm.GetComponent<Player>();
        switch (id)
        {
            case 0: // ��
                player.OnHeal(30);
                Heal healParticle = PoolManager.Instance.Pop("HealParticle") as Heal;
                healParticle.transform.position = GameManager.Instance.playerTrm.position;
                break;
            case 1: // speed
                player.OnSpeedUp(1f);
                break;
            case 2: // ���׳�
                player.OnMagnetUpgrade(1);
                MagnetParticle mParticle = PoolManager.Instance.Pop("MagnetParticle") as MagnetParticle;
                mParticle.transform.position = GameManager.Instance.playerTrm.position;
                break;
            case 3: // �н� ȣ��
                player.OnYarnSpawn(skillSO.list[id].upgradeLevel);
                break;
            case 4: // ����� ȣ��
                player.OnFishSpawn(skillSO.list[id].upgradeLevel);
                break;
            case 5: // �� ������
                player.OnPoopSpawn(skillSO.list[id].upgradeLevel);
                break;
            case 6: // ��ũ��ġ
                player.OnScratchOn(skillSO.list[id].upgradeLevel);
                break;
            case 7: // �θ޶�
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
