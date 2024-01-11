using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] lvlButtons;
    public Sprite starOn;
    private int buttonSelectedIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("LevelAt", 1);
        Debug.Log(levelAt);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelAt)
            {
                lvlButtons[i].interactable = false;
                lvlButtons[i].GetComponent<Outline>().enabled = false;
            }
            Transform stars = lvlButtons[i].transform.GetChild(0);
            for (int s = 0; s < 3; s++)
            {
                if (PlayerPrefs.GetInt("StarsLevel" + (i + 1), 0) > s)
                {
                    stars.GetChild(s).GetComponent<Image>().sprite = starOn;
                }
            }
        }

        foreach (Button button in lvlButtons)
        {
            button.gameObject.SetActive(false);
        }
        lvlButtons[0].gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        lvlButtons[buttonSelectedIndex].gameObject.SetActive(false);
        buttonSelectedIndex++;
        if(buttonSelectedIndex >= lvlButtons.Length)
        {
            buttonSelectedIndex = 0;
        }
        lvlButtons[buttonSelectedIndex].gameObject.SetActive(true);
    }

    public void PreviousLevel()
    {
        lvlButtons[buttonSelectedIndex].gameObject.SetActive(false);
        buttonSelectedIndex--;
        if (buttonSelectedIndex < 0)
        {
            buttonSelectedIndex = lvlButtons.Length - 1;
        }
        lvlButtons[buttonSelectedIndex].gameObject.SetActive(true);
    }
}
