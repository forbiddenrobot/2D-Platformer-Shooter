using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFruit : MonoBehaviour
{
    [SerializeField] private GameObject itemCollectedSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FruitCollected();
            GameObject soundEffect = Instantiate(itemCollectedSoundEffect, transform.position, transform.rotation);
            Destroy(soundEffect, 1f);
            Destroy(gameObject);
        }
    }

    private void FruitCollected()
    {
        GameObject.Find("GameManager").GetComponent<FruitManager>().FruitCollected();
    }
}
