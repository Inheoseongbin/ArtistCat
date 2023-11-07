using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private AgentAnimator anim;
    private SpriteRenderer sr;

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
            Hurt(10); 
        else if (collision.CompareTag("Boss"))
            Hurt(10);
    }

    private void Hurt(int dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (currentHP <= 0)
        {
            Die();
            return;
        }

        float val = 1.0f / (float)maxHP; // 0.01
        anim.SetHurt();
        healthBar.GaugeUI(val * currentHP);
    }

    private void Die()
    {
        if (!isDie)
        {
            anim.SetDead();
            print("�÷��̾���");
            isDie = true;

            UIManager.Instance.SetDeadUI();
        }
    }
}
