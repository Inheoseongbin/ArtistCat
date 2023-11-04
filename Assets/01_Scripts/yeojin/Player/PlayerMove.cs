using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Rigidbody2D rigid;
    private SpriteRenderer sr;
    private AgentAnimator anim;

    private void Awake()
    {
        anim = GetComponent<AgentAnimator>();
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

        // Anim&flipX
        if (movement.magnitude > 0)
        {
            anim.SetMove(true);
            sr.flipX = h > 0 ? true : false;
        }
        else anim.SetMove(false);
    }
}
