using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    [HideInInspector] public bool fire;
    [HideInInspector] public Transform target;
    [SerializeField] private float cooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] Transform firePoint;
    private float nextFireTime;
    [SerializeField] private GameObject bullet;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        firePoint.LookAt(target);

        if (fire && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + cooldown;
            animator.SetTrigger("attack");
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bulletGameObject = Instantiate(bullet, firePoint.transform.position, Quaternion.identity);
        EnemyBullet enemyBullet = bulletGameObject.GetComponent<EnemyBullet>();
        enemyBullet.moveTransform = target;
        enemyBullet.moveSpeed = bulletSpeed;
    }
}
