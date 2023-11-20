using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public static Action ShowUiBar;

    [SerializeField] private Slider _expBar;
    [SerializeField] private Slider _bossHpBar;

    private bool _isBarActive = false;

    private void Awake()
    {
        ShowUiBar += UiBar;
    }

    private void UiBar()
    {
        _expBar.gameObject.SetActive(_isBarActive);
        _isBarActive = !_isBarActive;
        _bossHpBar.gameObject.SetActive(_isBarActive);
    }
}
