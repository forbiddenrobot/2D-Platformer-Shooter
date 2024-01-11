using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Weapon : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float bulletDamage;
    private float nextFireTime;
    [SerializeField] private float bulletForce;
    [SerializeField] private float bulletTimeToLive;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Vector3 bulletRotationRight;
    [SerializeField] private Vector3 bulletRotationLeft;
    [SerializeField] private Transform playerVisual;

    private bool fire;
    private AudioSource shootingSoundEffect;

    private void Start()
    {
        nextFireTime = 0;
        shootingSoundEffect = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (fire && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + cooldown;
        }
    }

    private void Shoot()
    {
        GameObject bulletGameObject = Instantiate(bullet, firePoint.position, Quaternion.identity);
        bulletGameObject.GetComponent<PlayerBullet>().damageToDeal = bulletDamage;
        Rigidbody2D bulletRb = bulletGameObject.GetComponent<Rigidbody2D>();

        shootingSoundEffect.Play();

        if (playerVisual.rotation.y == 0)
        {
            bulletRb.velocity = new Vector2(bulletForce, 0f);
            bulletGameObject.transform.rotation = Quaternion.Euler(bulletRotationRight.x, bulletRotationRight.y, bulletRotationRight.z);
        }
        else
        {
            bulletRb.velocity = new Vector2(-bulletForce, 0f);
            bulletGameObject.transform.rotation = Quaternion.Euler(bulletRotationLeft.x, bulletRotationLeft.y, bulletRotationLeft.z);
        }

        PlayerBullet bulletScript = bulletGameObject.GetComponent<PlayerBullet>();
        bulletScript.timeToLive = bulletTimeToLive;
    }

    public void SetFire(bool fire)
    {
        this.fire = fire;
    }
}
