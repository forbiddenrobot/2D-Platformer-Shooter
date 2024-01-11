using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerConfigutation = GameObject.Find("PlayerConfigurationManager");

        if (playerConfigutation != null)
        {
            Destroy(playerConfigutation);
        }

        BackgroundMusic[] backgroundMusic = FindObjectsOfType<BackgroundMusic>();
        Debug.Log(backgroundMusic.Length);

        if (backgroundMusic.Length > 1)
        {
            for (int i = 1; i < backgroundMusic.Length; i++)
            {
                Destroy(backgroundMusic[i].gameObject);
            }
        }
    }
}
