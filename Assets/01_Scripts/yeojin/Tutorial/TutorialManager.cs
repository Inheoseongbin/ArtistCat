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
        // �׸� �׷��� ���� óġ���� ���
        infoText.text = "�Ӹ� ���� �׸���\n���� �׷� ���� óġ�ϼ���!";
        TMPTextTyping(1.5f);

        yield return new WaitUntil(() => e.GetComponent<Enemy>()._isDead);
        infoText.text = "WASD�� ������ ������ ���� �弼��.";
        TMPTextTyping(1.5f);

        GameObject ex = FindObjectOfType<Experience>().gameObject;
        // �� ex�� �Ծ��� ���
        infoText.text = "���� �������� ������ �������ϴ�.";


    }

    private void TMPTextTyping(float time) // Ÿ���� ���ִ� ��
    {
        infoText.maxVisibleCharacters = 0;
        DOTween.To(x => infoText.maxVisibleCharacters = (int)x, 0f, infoText.text.Length, time);
    }
}
