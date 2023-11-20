using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public static Action ShowUiBar;

    [SerializeField] private GameObject _expBar;
    [SerializeField] private GameObject _bossHpBar;

    private bool _isBarActive = false;

	private void Start()
	{
        ShowUiBar += UiBar;
    }

    private void UiBar()
    {
        //_isBarActive = !_isBarActive;
        _expBar.SetActive(!_expBar.activeSelf);
        _bossHpBar.SetActive(!_bossHpBar);
    }
}
