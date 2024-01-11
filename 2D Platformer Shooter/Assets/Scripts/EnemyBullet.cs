using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float timeToLive = 2.5f;
    [SerializeField] private bool autoAim;
    [HideInInspector] public Transform moveTransform;
    [HideInInspector] public float moveSpeed;
    private Vector3 moveLocation;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", timeToLive);

        moveLocation = moveTransform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(moveLocation, transform.position) < 0.1f)
        {
            Death();
        }

        if (autoAim)
        {
            moveLocation = moveTransform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, moveLocation, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            return;
        }

        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerLife>().DeathAnimation();
        }

        Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
