using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        Experience ex = FindObjectOfType<Experience>();
        yield return new WaitUntil(() => ex.IsSelected); // 먹었을 경우

        infoText.text = "적을 처치하여 별을 수집하세요!";
        TMPTextTyping(1.5f);

        Enemy[] enemys = new Enemy[3];
        for (int i = 0; i < 3; i++) 
        {
            Enemy e2 = PoolManager.Instance.Pop("BasicEnemy") as Enemy;
            enemys[i] = e2;

            float randSign = (Random.Range(0, 2) == 1) ? -1 : 1; // 무작위 부호 결정
            float randX = Random.Range(5, 16) * randSign;
            float randY = Random.Range(3, 10) * randSign;
            e2.transform.position = new Vector3(playerTrm.position.x + randX, playerTrm.position.y + randY, 0);
            e2.GetComponent<EnemyMovement>().enabled = true;
        }

        yield return new WaitForSeconds(4f);

        infoText.text = "일정량의 별이 모이게 된다면\n스킬을 선택할 수 있습니다.";
        TMPTextTyping(1.5f);

        yield return new WaitUntil(() => enemys.All(ens => ens._isDead));

        Experience[] exs = FindObjectsOfType<Experience>();
        yield return new WaitUntil(() => exs.All(ex => ex.IsSelected));
        print("dd");
        infoImage.SetActive(false); // 이걸 해줘야 함
        infoText.text = "";

        yield return new WaitUntil(() => !UIManager.Instance.IsSkillChooseOn);
        infoImage.SetActive(true);
        infoText.text = "튜토리얼 끝! 5초 뒤 자동으로 메인 화면으로 돌아갑니다.";
        TMPTextTyping(1.5f);
    }

    private void TMPTextTyping(float time) // 타이핑 해주는 거
    {
        infoText.maxVisibleCharacters = 0;
        DOTween.To(x => infoText.maxVisibleCharacters = (int)x, 0f, infoText.text.Length, time);
    }
}
