using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float timeToLive = 2.5f;
    public float damageToDeal;
    private string[] avoidCollisionsTag = { "Player", "Fruit", "Checkpoint", "Trap" };

    private void Start()
    {
        Invoke("Death", timeToLive);
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!avoidCollisionsTag.Contains(collision.tag))
        {
            if (collision.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyLife>().TakeDamage(damageToDeal);
            }
            Destroy(gameObject);
        }
    }
}
