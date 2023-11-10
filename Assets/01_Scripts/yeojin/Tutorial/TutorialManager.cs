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
    // ����~
    private IEnumerator EnemySpawnOnce()
    {
        Enemy e = PoolManager.Instance.Pop("BasicEnemy") as Enemy;
        e.transform.position = new Vector3(-10f, 0, 0);
        yield return new WaitForSeconds(2.0f);

        e.GetComponent<EnemyMovement>().enabled = false;
        // �׸� �׷��� ���� óġ���� ���
        infoText.text = "�Ӹ� ���� �׸���\n���� �׷� ���� óġ�ϼ���!";

        yield return new WaitUntil(() => e.GetComponent<Enemy>()._isDead);
        infoText.text = "������ ���� �弼��"; // ��ġ �����ϱ�
    }
}
