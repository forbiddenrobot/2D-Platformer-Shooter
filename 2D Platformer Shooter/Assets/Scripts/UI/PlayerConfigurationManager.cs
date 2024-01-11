using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    public static PlayerConfigurationManager Instance { get; private set; }

    private List<PlayerConfiguration> playerConfigs;

    [SerializeField] private int MaxPlayers = 2;
    [SerializeField] private List<Animator> animators;
    public List<Animator> notAviableAnimators;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Trying to create another Instance of PlayerConfigurationManager");
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
        playerConfigs = new List<PlayerConfiguration>();
        notAviableAnimators = new List<Animator>();
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerAnimatorController(int index, Animator controller)
    {
        playerConfigs[index].animatorController = controller;

        notAviableAnimators.Add(controller);

        int buttonSelectedIndex = animators.IndexOf(controller);
        Debug.Log(animators.IndexOf(controller));

        print(animators.IndexOf(controller));
        foreach (PlayerConfiguration playerConfiguration in playerConfigs)
        {
            if (playerConfigs[index] != playerConfiguration)
            {
                foreach (Transform buttonObject in playerConfiguration.menu)
                {
                    if (buttonObject.GetSiblingIndex() == buttonSelectedIndex)
                    {
                        UnityEngine.UI.Button button = buttonObject.GetComponent<UnityEngine.UI.Button>();
                        button.interactable = false;
                    }
                }
            }
        }
        
    }

    public void SetMenu(int index, Transform menu)
    {
        playerConfigs[index].menu = menu;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if (playerConfigs.All(p => p.IsReady == true))
        {
            // Everyone is ready
            SceneManager.LoadScene("Level Selector");
        }
    }

    public void HandlePlayerJoing(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            if (SceneManager.GetActiveScene().name == "PlayerSetup")
            {
                pi.transform.SetParent(transform);
                playerConfigs.Add(new PlayerConfiguration(pi));
            }
            else
            {
                Destroy(pi);
            }
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Animator animatorController { get; set; }   
    public Transform menu {  get; set; }
}
