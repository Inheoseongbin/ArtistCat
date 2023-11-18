using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Rigidbody2D rigid;
    private SpriteRenderer sr;
    private AgentAnimator anim;
    private PlayerHealth playerHealth;

    [SerializeField] private ParticleSystem _walkDust;

    public bool isFilp = false;

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
            if (h > 0)
            {
                isFilp = true;
                sr.flipX = true;
            }
            else if (h < 0)
            {
                isFilp = false;
                sr.flipX = false;
            }
            //sr.flipX = h > 0 ? true : false;

            if (!_walkDust.isPlaying)
                _walkDust.Play();
        }
        else anim.SetMove(false);
    }

    public void AddSpeed(float value)
    {
        speed += value;
    }
}
