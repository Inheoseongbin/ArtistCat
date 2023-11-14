using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level Instance;
    public int level = 1;
    private int experience = 0;

    int TO_LEVEL_UP
    {
        get { return level * 20; }
    }

    private void Awake()
    {
        if (Instance != null) print("������ũ��Ʈ instance ��������");
        Instance = this;
    }

    private void Update()
    {
        // ����׿�(���� ����)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddExperience(20);
        }
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            experience -= TO_LEVEL_UP;
            level += 1;
            UIManager.Instance.CheckingCanLevelUp();
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        UIManager.Instance.UpdateExp(experience, TO_LEVEL_UP, level);
    }
}
