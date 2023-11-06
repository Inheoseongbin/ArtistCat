using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMove(bool value)
    {
        animator.SetBool("Move", value);
    }

    public void SetDead()
    {
        animator.SetTrigger("Dead");
    }

    public void SetHurt(bool value)
    {
        animator.SetBool("Hurt", value);
    }
}
