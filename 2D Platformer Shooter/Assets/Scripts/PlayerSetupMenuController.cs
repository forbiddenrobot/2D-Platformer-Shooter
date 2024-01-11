using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI readyText;
    [SerializeField] private List<Animator> animators;

    private float ignoreInputTime = 0.5f;
    private bool inputEnabled;

    [SerializeField] private MultiplayerEventSystem multiplayerEventSystem;

    public void SetPlayerIndex(int playerIndex)
    {
        this.playerIndex = playerIndex;
        titleText.text = "Choose Your Characted Player " + (playerIndex + 1);
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetAnimator(Animator animatorController)
    {
        //if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.SetPlayerAnimatorController(playerIndex, animatorController);
        readyText.text = "Ready Player " + (playerIndex + 1);
        readyText.gameObject.SetActive(true);

        ReadyPlayer();

        Transform menu = transform.GetChild(0);
        int buttonSelectedIndex = animators.IndexOf(animatorController);
        foreach (Transform child in menu)
        {
            if (child.GetSiblingIndex() != buttonSelectedIndex)
            {
                Button button = child.GetComponent<Button>();
                button.interactable = false;
                Debug.Log(child.gameObject);
            }
        }
    }

    public void ReadyPlayer()
    {
        //if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
    }
}
