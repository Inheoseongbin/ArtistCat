using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fadeImg;
    private bool isFirstClick = false;

    private void Start()
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.DOFade(0, 0.8f);
    }

    public void LoadGameScene()
    {
        if (isFirstClick) return;
        fadeImg.DOFade(1, 0.8f).OnComplete(() => SceneManager.LoadScene(2));
        
    }
    public void LoadTutorialScene()
    {
        if (isFirstClick) return;
        fadeImg.DOFade(1, 0.8f).OnComplete(() => SceneManager.LoadScene(1));
    }
}
