using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    // 0¿Ã ∏ﬁ¿Œ æ¿
    public void LoadGameScene()
    {
        SceneManager.LoadScene(2); // 2∞° ∞‘¿” æ¿
    }
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(1); // 1¿Ã ∆©≈‰∏ÆæÛ æ¿
    }
}
