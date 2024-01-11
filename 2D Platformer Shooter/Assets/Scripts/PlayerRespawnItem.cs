using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnItem : MonoBehaviour
{
    [SerializeField] private GameObject itemCollectedSoundEffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.PlayerRespawn(transform);
            GameObject soundEffect = Instantiate(itemCollectedSoundEffect, transform.position, transform.rotation);
            Destroy(soundEffect, 1f);
            Destroy(gameObject);
        }
    }
}
