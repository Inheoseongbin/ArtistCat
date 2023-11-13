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
        // �׸� �׷��� ���� óġ���� ���
        infoText.text = "�Ӹ� ���� �׸���\n���� �׷� ���� óġ�ϼ���!";
        TMPTextTyping(1.5f);

        yield return new WaitUntil(() => e1.GetComponent<Enemy>()._isDead);
        infoText.text = "WASD�� ������ ������ ���� �弼��.";
        TMPTextTyping(1.5f);

        GameObject ex = FindObjectOfType<Experience>().gameObject;
        // �� ex�� �Ծ��� ��� (waitfor �̰Ŵ� ����)
        yield return new WaitForSeconds(3f);
        infoText.text = "���� óġ�Ͽ� ���� �����ϼ���!";
        TMPTextTyping(1.5f);
        yield return new WaitForSeconds(1f); // ��ٷȴٰ�

        for (int i = 0; i < 3; i++) 
        {
            Enemy e2 = PoolManager.Instance.Pop("BasicEnemy") as Enemy;

            float randSign = (Random.Range(0, 2) == 0) ? -1 : 1; // ������ ��ȣ ����
            float rand1 = Random.Range(8, 13) * randSign; // ��ȣ�� �����Ͽ� 8�� 12 ���� Ȥ�� -8�� -12 ���̿��� ����
            e2.transform.position = new Vector3(playerTrm.position.x + rand1, playerTrm.position.y, 0);
            e2.GetComponent<EnemyMovement>().enabled = true;

            yield return new WaitUntil(() => e2.GetComponent<Enemy>()._isDead);
        }

        // ���� �� ���̰� �Ǹ� �� �ؽ�Ʈ ����ְ�
        infoText.text = "�������� ���� ���̰� �ȴٸ�\n��ų�� ������ �� �ֽ��ϴ�.";
        TMPTextTyping(1.5f);
        infoImage.transform.DOLocalMoveY(350, 1.5f);
        
    }

    private void TMPTextTyping(float time) // Ÿ���� ���ִ� ��
    {
        infoText.maxVisibleCharacters = 0;
        DOTween.To(x => infoText.maxVisibleCharacters = (int)x, 0f, infoText.text.Length, time);
    }
}
