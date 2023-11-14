using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneTransform : MonoBehaviour
{
    private SpriteRenderer sr;

    public void FadeIn(int sceneNum)
    {
        sr = GetComponent<SpriteRenderer>();
        Color newColor = new Color(0, 0, 0, 0);
        sr.color = newColor;
        sr.DOFade(1, 2f).OnComplete(() => SceneManager.LoadScene(sceneNum));
    }

    public void FadeOut(int sceneNum)
    {
        sr = GetComponent<SpriteRenderer>();
        Color newColor = new Color(0, 0, 0, 1);
        sr.color = newColor;
        sr.DOFade(0, 2f).OnComplete(() => SceneManager.LoadScene(sceneNum));
    }
}
