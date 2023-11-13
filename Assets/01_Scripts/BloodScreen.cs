using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScreen : MonoBehaviour
{
    [SerializeField]
    private float time = 0.3f;
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        img.enabled = false;
    }

    public void ShowBloodScreen()
    {
        StopAllCoroutines();
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine()
    {
        img.enabled = true;
        yield return new WaitForSeconds(time);
        img.enabled = false;
    }
}
