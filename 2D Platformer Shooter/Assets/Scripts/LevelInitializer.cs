using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PlayerConfiguration[] playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            GameObject player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
        }
    }
}
