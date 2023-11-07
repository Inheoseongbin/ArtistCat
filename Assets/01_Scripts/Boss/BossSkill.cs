using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : BossMain
{
    [SerializeField] private float _shootCool;
    [SerializeField] private float _dashCool;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _dashImage;

    private float _stunCool = 2f;

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

    private void Update()
    {
        
    }

    IEnumerator DashRoutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(3);

            _bossValue._isDash = true;

            viewDir = _bossValue._playerTr.transform.position - transform.position;

            float angle = Mathf.Atan2(viewDir.y, viewDir.x) * Mathf.Rad2Deg;

            _dashImage.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle + 90));
            _dashImage.SetActive(true);

            yield return new WaitForSeconds(3);

            _dashImage.SetActive(false);

            _rb.AddForce(viewDir.normalized * _bossValue._DashSpeed);
            _bossValue._isDash = false;
        }
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
        Destroy(bullet, 2.0f);
    }
}
