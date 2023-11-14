using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadeInOutSR : MonoBehaviour
{
    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void FadeIn()
    {
        sr.DOFade(1, 1.5f);
    }

    public void FadeOut()
    {
        sr.DOFade(0, 1.5f);
    }
}
