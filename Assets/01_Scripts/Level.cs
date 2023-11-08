using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;

    int TO_LEVEL_UP
    {
        get { return level * 20; }
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            experience -= TO_LEVEL_UP;
            level += 1;
            UIManager.Instance.SkillRandomChoose();
        }
    }
}
