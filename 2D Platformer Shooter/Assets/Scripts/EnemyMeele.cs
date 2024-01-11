using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeele : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Attack(collision.gameObject);
        }
    }

    private void Attack(GameObject player)
    {
        if (player != null)
        {
            animator.SetTrigger("attack");
            player.GetComponent<PlayerLife>().DeathAnimation();
        }
    }
}
