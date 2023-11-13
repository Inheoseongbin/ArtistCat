using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private GameObject infoImage;

    private void Start()
    {
        infoText.text = "";
        StartCoroutine(TutorialStart());
    }

    private IEnumerator TutorialStart()
    {
        Enemy e1 = PoolManager.Instance.Pop("BasicEnemy") as Enemy;

        Transform playerTrm = GameManager.Instance.playerTrm;
        e1.transform.position = new Vector3(playerTrm.position.x - 5f, playerTrm.position.y, 0);
        yield return new WaitForSeconds(1.0f);

        e1.GetComponent<EnemyMovement>().enabled = false;
        // 그림 그려서 적을 처치했을 경우
        infoText.text = "머리 위의 그림을\n따라 그려 적을 처치하세요!";
        TMPTextTyping(1.5f);

        yield return new WaitUntil(() => e1.GetComponent<Enemy>()._isDead);
        infoText.text = "WASD로 움직여 떨어진 별을 드세요.";
        TMPTextTyping(1.5f);

        GameObject ex = FindObjectOfType<Experience>().gameObject;
        // 이 ex를 먹었을 경우 (waitfor 이거는 임의)
        yield return new WaitForSeconds(3f);
        infoText.text = "적을 처치하여 별을 수집하세요!";
        TMPTextTyping(1.5f);
        yield return new WaitForSeconds(1f); // 기다렸다가

        for (int i = 0; i < 3; i++) 
        {
            Enemy e2 = PoolManager.Instance.Pop("BasicEnemy") as Enemy;

            float randSign = (Random.Range(0, 2) == 0) ? -1 : 1; // 무작위 부호 결정
            float rand1 = Random.Range(8, 13) * randSign; // 부호를 적용하여 8과 12 사이 혹은 -8과 -12 사이에서 생성
            e2.transform.position = new Vector3(playerTrm.position.x + rand1, playerTrm.position.y, 0);
            e2.GetComponent<EnemyMovement>().enabled = true;

            yield return new WaitUntil(() => e2.GetComponent<Enemy>()._isDead);
        }

        // 별이 다 모이게 되면 이 텍스트 띄워주고
        infoText.text = "일정량의 별이 모이게 된다면\n스킬을 선택할 수 있습니다.";
        TMPTextTyping(1.5f);
        infoImage.transform.DOLocalMoveY(350, 1.5f);
        
    }

    private void TMPTextTyping(float time) // 타이핑 해주는 거
    {
        infoText.maxVisibleCharacters = 0;
        DOTween.To(x => infoText.maxVisibleCharacters = (int)x, 0f, infoText.text.Length, time);
    }
}
