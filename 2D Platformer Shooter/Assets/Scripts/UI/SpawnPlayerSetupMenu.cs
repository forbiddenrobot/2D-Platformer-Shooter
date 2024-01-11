using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    [SerializeField] private List<Animator> animators;

    public GameObject playerSetupMenuPrefab;
    public PlayerInput input;

    private void Awake()
    {
        GameObject rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            GameObject setupMenu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = setupMenu.GetComponentInChildren<InputSystemUIInputModule>();
            setupMenu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
            GameObject buttonMenu = setupMenu.transform.GetChild(0).gameObject;
            PlayerConfigurationManager.Instance.SetMenu(input.playerIndex, buttonMenu.transform);

            foreach (Animator controller in PlayerConfigurationManager.Instance.notAviableAnimators)
            {
                int indexOfButton = animators.IndexOf(controller);
                Debug.Log(indexOfButton);
                foreach (Transform buttonObject in buttonMenu.transform)
                {
                    if (buttonObject.GetSiblingIndex() == indexOfButton)
                    {
                        UnityEngine.UI.Button button = buttonObject.GetComponent<UnityEngine.UI.Button>();
                        button.interactable = false;
                    }
                }
            }
        }
    }
}
