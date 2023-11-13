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
        // �׸� �׷��� ���� óġ���� ���
        infoText.text = "�Ӹ� ���� �׸���\n���� �׷� ���� óġ�ϼ���!";
        TMPTextTyping(1.5f);

        yield return new WaitUntil(() => e1.GetComponent<Enemy>()._isDead);
        infoText.text = "WASD�� ������ ������ ���� �弼��.";
        TMPTextTyping(1.5f);

        Experience ex = FindObjectOfType<Experience>();
        yield return new WaitUntil(() => ex.IsSelected); // �Ծ��� ���

        infoText.text = "���� óġ�Ͽ� ���� �����ϼ���!";
        TMPTextTyping(1.5f);

        Enemy[] enemys = new Enemy[3];
        for (int i = 0; i < 3; i++) 
        {
            Enemy e2 = PoolManager.Instance.Pop("BasicEnemy") as Enemy;
            enemys[i] = e2;

            float randSign = (Random.Range(0, 2) == 1) ? -1 : 1; // ������ ��ȣ ����
            float randX = Random.Range(5, 16) * randSign;
            float randY = Random.Range(3, 10) * randSign;
            e2.transform.position = new Vector3(playerTrm.position.x + randX, playerTrm.position.y + randY, 0);
            e2.GetComponent<EnemyMovement>().enabled = true;
        }

        yield return new WaitForSeconds(4f);

        infoText.text = "�������� ���� ���̰� �ȴٸ�\n��ų�� ������ �� �ֽ��ϴ�.";
        TMPTextTyping(1.5f);

        yield return new WaitUntil(() => enemys.All(ens => ens._isDead));

        Experience[] exs = FindObjectsOfType<Experience>();
        yield return new WaitUntil(() => exs.All(ex => ex.IsSelected));
        print("dd");
        infoImage.SetActive(false); // �̰� ����� ��
        infoText.text = "";

        yield return new WaitUntil(() => !UIManager.Instance.IsSkillChooseOn);
        infoImage.SetActive(true);
        infoText.text = "Ʃ�丮�� ��! 5�� �� �ڵ����� ���� ȭ������ ���ư��ϴ�.";
        TMPTextTyping(1.5f);
    }

    private void TMPTextTyping(float time) // Ÿ���� ���ִ� ��
    {
        infoText.maxVisibleCharacters = 0;
        DOTween.To(x => infoText.maxVisibleCharacters = (int)x, 0f, infoText.text.Length, time);
    }
}
