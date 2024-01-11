using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTouchTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerLife playerLife = collision.GetComponent<PlayerLife>();
            playerLife.DeathAnimation();
        }
        else if (collision.tag == "Enemy")
        {
            EnemyLife enemyLife = collision.GetComponent<EnemyLife>();
            enemyLife.TakeDamage(Mathf.Infinity);
        }
    }
}
