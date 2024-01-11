using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private Sprite starOn;
    [SerializeField] private FruitManager fruitManager;
    private Animator animator;
    private AudioSource levelCompletedSoundEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
        levelCompletedSoundEffect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("End_Collected"))
            {
                levelCompletedSoundEffect.Play();
                animator.SetTrigger("collected");
            }
        }
    }

    public void EndAnimationEnded()
    {
        int levelAt = PlayerPrefs.GetInt("LevelAt", 1);
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring("Level ".Length));
        if (currentLevel + 1 > levelAt)
        {
            levelAt = currentLevel + 1;
            PlayerPrefs.SetInt("LevelAt", levelAt);
        }

        int starsHad = PlayerPrefs.GetInt("StarsLevel" + currentLevel, 0);
        int starsGot = fruitManager.StarsGot();
        Debug.Log(starsGot);
        if (starsGot > starsHad)
        {
            PlayerPrefs.SetInt("StarsLevel" + currentLevel, starsGot);
        }
        Time.timeScale = 0f;
        levelCompletedPanel.SetActive(true);
        levelCompletedPanel.transform.GetChild(3).transform.GetChild(0).GetComponent<Button>().Select();

        Transform stars = levelCompletedPanel.transform.GetChild(1);
        for (int s = 0; s < 3; s++)
        {
            if (starsGot > s)
            {
                Debug.Log(starsGot);
                stars.GetChild(s).GetComponent<Image>().sprite = starOn;
            }
        }
    }
}
