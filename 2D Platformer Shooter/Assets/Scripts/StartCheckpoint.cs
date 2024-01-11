using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddCheckpoint(transform);
        Time.timeScale = 1.0f;
    }
}
