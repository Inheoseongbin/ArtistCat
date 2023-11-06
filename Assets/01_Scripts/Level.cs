using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private ExperienceBar expBar;

    int level = 1;
    int experience = 0;

    int TO_LEVEL_UP
    {
        get { return level * 50; }
    }

    private void Start()
    {
        expBar.UpdateExpBar(experience, TO_LEVEL_UP);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        expBar.UpdateExpBar(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            experience -= TO_LEVEL_UP;
            level += 1;
        }
    }
}
