using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void DeathAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Death"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            deathSoundEffect.Play();
            animator.SetTrigger("death");
        }
    }
}
