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
    [SerializeField] protected float _knockPower;

    [Header("������Ʈ")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _dashImage;

    protected bool _isAiming = false;
    protected bool _isKnock = false;

    private float _stunCool = 2f;
    private float dTime;

    private Vector3 viewDir = Vector3.zero;

    private List<Bullet> saveBulletList = new List<Bullet>();
    private List<float> BulletAngleList = new List<float>();
    private Bullet bullet = null;

    int angleCount = 12;
    int defaultAngle;
    protected override void Awake()
    {
        base.Awake();

        defaultAngle = 360 / angleCount;

        _bossValue._isDash = false;

        _bossValue._playerTr = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (_isAiming)
            DashAiming();

        if(_isKnock)
        {
            _bossValue._playerTr.gameObject.GetComponent<Rigidbody2D>().AddForce(viewDir * _knockPower, ForceMode2D.Impulse);
            _isKnock = false;
        }
    }

    public void Attack()
    {
        StopAllCoroutines();
        StartCoroutine(ShootRoutine());
        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine()
    {
        dTime = EnemySpawner.Instance.bossTypes._dashCool - _waitDashTime - _dashingTime;
        while (true)
        {
            yield return new WaitForSeconds(dTime);

            ReadyDash();

            yield return new WaitForSeconds(_waitDashTime);

            _isAiming = false;

            yield return new WaitForSeconds(0.5f);

            Dashing();

            yield return new WaitForSeconds(_dashingTime);

            _bossValue._isDash = false;
        }
    }
    #region �뽬 ���
    private void ReadyDash()
    {
        _bossValue._isDash = true;
        _isAiming = true;
    }

    private void DashAiming()
    {
        viewDir = _bossValue._playerTr.position - transform.position;

        float angle = Mathf.Atan2(viewDir.y, viewDir.x) * Mathf.Rad2Deg;

        _dashImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        if (!EnemySpawner.Instance.isBossDead)
            _dashImage.SetActive(true);
    }

    private void Dashing()
    {
        _dashImage.SetActive(false);

        _rb.velocity = viewDir.normalized * _bossValue._DashSpeed;
    }
    #endregion

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemySpawner.Instance.bossTypes._shootCool - _stunCool);

            _bossValue._isSkill = true;
            saveBulletList.Clear();

            _animator.SetTrigger("attack");

            for (int i = 0; i < angleCount; i++)
            {
                ShootBullet(defaultAngle * i, transform.position);
                BulletAngleList.Add(defaultAngle * i);
            }
            if (EnemySpawner.Instance.bossTypes.Count == 3)
                StartCoroutine(LastBossShoot());

            yield return new WaitForSeconds(_stunCool);

            //�ٽ� ������
            _bossValue._isSkill = false;
        }
    }

    void ShootBullet(float angle, Vector2 pos)
    {
        Shoot(angle, pos);
        saveBulletList.Add(bullet);
    }

    private void Shoot(float angle, Vector2 pos)
    {
        // �Ѿ��� �߻��� ������ �������� ��ȯ
        float radianAngle = angle * Mathf.Deg2Rad;

        // �Ѿ��� ���� ���� ���
        Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

        // �Ѿ� ���� �� ����
        bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
        bullet.transform.position = pos;


        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * EnemySpawner.Instance.bossTypes._bulletSpeed;
    }

    IEnumerator LastBossShoot()
    {
        yield return new WaitForSeconds(1);
        foreach (var b in saveBulletList)
        {
            //Bullet bullet = b;
            b.BulletPool();
            for (int i = 0; i < 12; i++)
            {
                if (i % 2 == 0)
                    Shoot(BulletAngleList[i], b.transform.position);
            }
        }
        BulletAngleList.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_bossValue._isDash)
                Knockback(collision.gameObject, viewDir);
        }
    }

    private void Knockback(GameObject colObj, Vector2 knockDir)
    {
        _isKnock = true;
     //   colObj.gameObject.GetComponent<Rigidbody2D>().AddForce(knockDir * _knockPower, ForceMode2D.Impulse);
        
        _rb.velocity = Vector2.zero;
    }
}
