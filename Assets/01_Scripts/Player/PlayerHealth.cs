using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    private AgentAnimator anim;
    private SpriteRenderer sr;
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    // HP
    private int maxHP = 100;
    private int currentHP = 0;
    public int PlayerHP
    {
        get => currentHP;
        set => currentHP = value;
    }

    private bool isDie = false;

    public int CurrentHP => currentHP;
    public bool IsDie => isDie;

    [SerializeField] private HealthBarUI healthBar;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); // ������ ������ �ϰ�
        anim = GetComponent<AgentAnimator>(); // ������ Hurt animation ����ϴ°�
    }

    private void Start()
    {
        currentHP = maxHP; // �ʱ� HP ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
		{
            Hurt(10); 
		}
        else if (collision.CompareTag("Boss"))
		{
            Hurt(10);

		}
        else if (collision.CompareTag("Bullet"))
            Hurt(10);
    }

    private void Hurt(int dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        StartCoroutine(Hit());
        OnGetHit?.Invoke();

        if (currentHP <= 0)
        {
            Die();
            return;
        }

        float val = 1.0f / (float)maxHP; // 0.01
        anim.SetHurt();
        healthBar.GaugeUI(val * currentHP);
    }

    private IEnumerator Hit()
    {
        sr.material.SetInt("_IsSolidColor", 1);
        yield return new WaitForSeconds(.1f);
        sr.material.SetInt("_IsSolidColor", 0);
    }

    public void AddHP(int heal)
    {
        currentHP += heal;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        float val = 1.0f / (float)maxHP; // 0.01
        healthBar.GaugeUI(val * currentHP);
    }

    private void Die()
    {
        if (!isDie)
        {
            anim.SetDead();
            isDie = true;
            GameManager.Instance.GameOver();
            UIManager.Instance.SetDeadUI();
        }
    }
}
