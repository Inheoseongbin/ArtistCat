using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;

    private void Start()
    {
        infoText.text = "";
        StartCoroutine(TutorialStart());
    }

    private IEnumerator TutorialStart()
    {
        Enemy e = PoolManager.Instance.Pop("BasicEnemy") as Enemy;
        e.transform.position = new Vector3(-10f, 0, 0);
        yield return new WaitForSeconds(2.0f);

        e.GetComponent<EnemyMovement>().enabled = false;
        // 그림 그려서 적을 처치했을 경우
        infoText.text = "머리 위의 그림을\n따라 그려 적을 처치하세요!";
        TMPTextTyping(1.5f);

        yield return new WaitUntil(() => e.GetComponent<Enemy>()._isDead);
        infoText.text = "WASD로 움직여 떨어진 별을 드세요.";
        TMPTextTyping(1.5f);

        GameObject ex = FindObjectOfType<Experience>().gameObject;
        // 이 ex를 먹었을 경우
        infoText.text = "별을 모을수록 레벨이 높아집니다.";


    }

    private void TMPTextTyping(float time) // 타이핑 해주는 거
    {
        infoText.maxVisibleCharacters = 0;
        DOTween.To(x => infoText.maxVisibleCharacters = (int)x, 0f, infoText.text.Length, time);
    }
}
