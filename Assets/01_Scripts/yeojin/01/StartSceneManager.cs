using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private SceneTransform fadeImage;
    private bool isFirstClick = false;

    public void LoadGameScene()
    {
        if (isFirstClick) return;
        fadeImage.FadeIn(2);
    }
    public void LoadTutorialScene()
    {
        if (isFirstClick) return;
        fadeImage.FadeIn(1);
    }
}
