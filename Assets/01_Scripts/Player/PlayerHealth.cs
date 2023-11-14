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
    private int maxHP = 200;
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
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<AgentAnimator>(); 
    }

    private void Start()
    {
        currentHP = maxHP; 
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Enemy"))
        {
            int enemyLevel = collision.gameObject.GetComponent<Enemy>().typeCount; // 레벨 별로 데미지 다르게 해주기
            switch (enemyLevel)
            {
                case 1:
                    Hurt(2);
                    break;
                case 2: // 
                    Hurt(5);
                    break;
                case 4: //
                    Hurt(9);
                    break;
                case 5: // 
                    Hurt(12);
                    break;
            }
        }
       else if (collision.CompareTag("Bullet"))
            Hurt(5);
        else if (collision.CompareTag("Boss"))
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
