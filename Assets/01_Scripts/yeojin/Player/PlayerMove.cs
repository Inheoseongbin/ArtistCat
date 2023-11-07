using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    public float PlayerSpeed
    {
        get => speed;
        set => speed = value;
    }
    private Rigidbody2D rigid;
    private SpriteRenderer sr;
    private AgentAnimator anim;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        anim = GetComponent<AgentAnimator>();
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (!playerHealth.IsDie)
        {
            Move();
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }
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
            if(h > 0)
                sr.flipX = true;
            else if(h < 0) 
                sr.flipX = false;
            //sr.flipX = h > 0 ? true : false;
        }
        else anim.SetMove(false);
    }
}
