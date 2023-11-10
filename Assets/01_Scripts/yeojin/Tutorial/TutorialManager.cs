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
        StartCoroutine(EnemySpawnOnce());
    }
    // 스폰~
    private IEnumerator EnemySpawnOnce()
    {
        Enemy e = PoolManager.Instance.Pop("BasicEnemy") as Enemy;
        e.transform.position = new Vector3(-10f, 0, 0);
        yield return new WaitForSeconds(2.0f);

        e.GetComponent<EnemyMovement>().enabled = false;
        // 그림 그려서 적을 처치했을 경우
        infoText.text = "머리 위의 그림을\n따라 그려 적을 처치하세요!";

        yield return new WaitUntil(() => e.GetComponent<Enemy>()._isDead);
        infoText.text = "떨어진 별을 드세용"; // 위치 수정하긔
    }
}
