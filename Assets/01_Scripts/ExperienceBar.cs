using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private Slider expBar;

    public void UpdateExpBar(int current, int target)
    {
        expBar.maxValue = target;
        expBar.value = current;
    }
}
