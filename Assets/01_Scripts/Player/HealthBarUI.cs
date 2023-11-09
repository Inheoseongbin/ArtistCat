using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform bar;
    public void GaugeUI(float val)
    {
        bar.transform.localScale = new Vector3(val, 1, 1);
    }
}
