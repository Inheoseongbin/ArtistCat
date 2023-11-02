using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(h, v, 0);
        
        rigid.velocity = movement * speed;

        // Anim
        if (movement.magnitude > 0)
        {
            anim.SetBool("Move", true);
            sr.flipX = h > 0 ? true : false;
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }

    private void Dead()
    {
        anim.SetTrigger("Dead");
    }

    private void Hurt()
    {
        // HP ���
        anim.SetBool("Hurt", true); // 0.5�� �Ŀ� �ٽ� false �Ǿ�� ��
    }
}
