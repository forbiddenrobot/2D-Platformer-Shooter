using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    private AudioSource gravitySwitchSound;

    private void Start()
    {
        gravitySwitchSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SwitchGravity(collision.gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyLife>().TakeDamage(Mathf.Infinity);
        }
    }

    private void SwitchGravity(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb.gravityScale > 0)
        {
            rb.gravityScale = -1;
            player.transform.localScale = new Vector3(player.transform.localScale.x, -1, 1);
        }
        else
        {
            rb.gravityScale = 1;
            player.transform.localScale = new Vector3(player.transform.localScale.x, 1, 1);
        }
        player.transform.position = transform.position;
        gravitySwitchSound.Play();
    }
}
