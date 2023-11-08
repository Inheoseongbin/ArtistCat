using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : BossMain
{
    [Header("��Ÿ��")]
    [SerializeField] private float _shootCool;
    [SerializeField] private float _dashCool;

    [Header("�Ѿ�")]
    [SerializeField] private float _bulletSpeed;

    [Header("�뽬")]
    [SerializeField] protected float _waitDashTime;
    [SerializeField] protected float _dashingTime;

    [Header("������Ʈ")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _dashImage;

    protected bool _isCharging = false;

    private float _stunCool = 2f;
    private float dTime;

    private Vector3 viewDir = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();

        _bossValue._isDash = false;

        _bossValue._playerTr = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start()
    {
        StartCoroutine(ShootRoutine());
        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine()
    {
        dTime = _dashCool - _waitDashTime - _dashingTime;
        while (true)
        {
            yield return new WaitForSeconds(dTime);

            _bossValue._isDash = true;

            viewDir = _bossValue._playerTr.position - transform.position;
            _bossValue.lookDir = viewDir;

            float angle = Mathf.Atan2(viewDir.y, viewDir.x) * Mathf.Rad2Deg;

            _dashImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            _dashImage.SetActive(true);

            yield return new WaitForSeconds(_waitDashTime);

            _dashImage.SetActive(false);
            _isCharging = true;

            _rb.velocity = viewDir.normalized * _bossValue._DashSpeed;

            yield return new WaitForSeconds(_dashingTime);

            _bossValue._isDash = false;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, viewDir, Color.red);
    }

    IEnumerator ShootRoutine()
    {
        int angleCount = 12;
        int angle = 360 / angleCount;
        while (true)
        {
            _bossValue._isSkill = true;

            for (int i = 0; i < angleCount; i++)
            {
                ShootBullet(angle * i);
            }

            yield return new WaitForSeconds(_stunCool);

            //�ٽ� ������
            _bossValue._isSkill = false;

            yield return new WaitForSeconds(_shootCool - _stunCool);
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
        Destroy(bullet, 10.0f);
    }
}