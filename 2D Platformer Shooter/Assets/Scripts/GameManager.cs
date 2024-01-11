using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject levelInitializer;
    public static GameManager instance;
    private List<GameObject> allPlayers;
    private List<Transform> checkpoints;
    private List<PlayerConfiguration> deadPlayerConfigurations;

    [SerializeField] private GameObject pauseMenu;
    private bool gamePaused = false;

    private void Awake()
    {
        instance = this;

        checkpoints = new List<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lives = 0;
        livesText.text = "Lives: 0";

        allPlayers = GameObject.FindGameObjectsWithTag("Player").ToList();
        deadPlayerConfigurations = new List<PlayerConfiguration>();
    }

    public void PlayerDied(GameObject player)
    {
        PlayerInputHandler playerInputHandler = player.GetComponent<PlayerInputHandler>();
        Debug.Log(playerInputHandler);
        PlayerConfiguration playerConfig = playerInputHandler.GetPlayerConfig();
        deadPlayerConfigurations.Add(playerConfig);
        playerInputHandler.RemoveActionTriggered();
        Destroy(player);
        if (lives > 0)
        {
            lives -= 1;
            UpdateLivesText();
            PlayerRespawn(checkpoints[0]);
        }
        else if (deadPlayerConfigurations.Count >= allPlayers.Count)
        {
            RestartLevel();
        }
    }

    public void PlayerRespawn(Transform position)
    {
        if (deadPlayerConfigurations.Count > 0)
        {
            GameObject player = Instantiate(playerPrefab, position.position, Quaternion.identity, levelInitializer.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(deadPlayerConfigurations[0]);
            deadPlayerConfigurations.RemoveAt(0);
        }
        else
        {
            lives += 1;
            UpdateLivesText();
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddCheckpoint(Transform location)
    {
        checkpoints.Insert(0, location);
    }

    public void PauseGame()
    {
        if (gamePaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            gamePaused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            pauseMenu.transform.GetChild(1).transform.GetChild(0).GetComponent<Button>().Select();
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }
}
