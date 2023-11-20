using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;

    public void UpdateExpBar(int current, int target)
    {
        _hpBar.value = current;
        _hpBar.maxValue = target;
    }
}
