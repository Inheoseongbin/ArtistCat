using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : BossMain
{
    [SerializeField] private float _skillCool;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;

    private Vector2 _shootDir = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SkillRoutine());
    }

    IEnumerator SkillRoutine()
    {
        int angleCount = 12;
        int angle = 360 / angleCount;
        while (true)
        {
            for(int i = 0; i < angleCount; i++)
            {
                ShootBullet(angle * i);
            }

            yield return new WaitForSeconds(_skillCool);
        }
    }

    void ShootBullet(float angle)
    {
        // �Ѿ��� �߻��� ������ �������� ��ȯ
        float radianAngle = angle * Mathf.Deg2Rad;

        // �Ѿ��� ���� ���� ���
        Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

        // �Ѿ� ���� �� ����
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * _bulletSpeed;

        // �Ѿ� ���� �ð� �Ŀ� �ı�
        Destroy(bullet, 2.0f);
    }
}
