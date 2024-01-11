using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameObject deathEffect;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.5f);
        Destroy(gameObject);
    }
}
