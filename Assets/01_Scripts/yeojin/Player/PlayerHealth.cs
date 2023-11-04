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
    public int CurrentHP => currentHP;

    [SerializeField] private HealthBarUI healthBar;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); // 맞으면 빨갛게 하고
        anim = GetComponent<AgentAnimator>(); // 맞으면 Hurt animation 재생하는거
    }

    private void Start()
    {
        currentHP = maxHP; // 초기 HP 설정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurt(10);
    }

    private void Hurt(int dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if(currentHP <= 0)
        {
            Die();
            return;
        }

        float val = 1.0f / (float)maxHP; // 0.01
        healthBar.GaugeUI(val * currentHP); 
    }

    private void Die()
    {
        print("플레이어죽");
    }
}
