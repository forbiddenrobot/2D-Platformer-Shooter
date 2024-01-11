using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    private PlayerMovement playerMovement;
    private PlayerWeaponsSelection playerWeaponsSelection;
    private PlayerConfiguration playerConfig;
    private GameObject weapons;
    private PlayerInputActions playerInputActions;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInputActions = new PlayerInputActions();

        playerWeaponsSelection = playerMovement.gameObject.GetComponent<PlayerWeaponsSelection>();
        weapons = playerMovement.transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        animator.runtimeAnimatorController = pc.animatorController.runtimeAnimatorController;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == playerInputActions.Player.HorizontalMovement.name)
        {
            OnMove(context);
        }
        else if (context.action.name == playerInputActions.Player.Firing.name)
        {
            OnFire(context);
        }
        else if (context.action.name == playerInputActions.Player.Jump.name)
        {
            Jump(context);
        }
        else if (context.action.name == playerInputActions.Player.NextWeapon.name)
        {
            NextWeapon(context);
        }
        else if (context.action.name == playerInputActions.Player.PreviousWeapon.name)
        {
            PreviousWeapon(context);
        }
        else if (context.action.name == playerInputActions.Player.Pause.name)
        {
            Pause(context);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementVector(context.ReadValue<Vector2>());
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        float firingValue = context.ReadValue<float>();
        bool fire = firingValue > 0;
        foreach(Transform weapon in weapons.transform)
        {
            weapon.GetComponent<Weapon>().SetFire(fire);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        float jumpValue = context.ReadValue<float>();
        bool jump = jumpValue > 0;
        playerMovement.SetJumping(jump);
    }

    public void NextWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerWeaponsSelection.NextWeapon();
        }
    }

    public void PreviousWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerWeaponsSelection.PreviousWeapon();
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            GameManager.instance.PauseGame();
        }
    }

    public void RemoveActionTriggered()
    {
        playerConfig.Input.onActionTriggered -= Input_onActionTriggered;
    }

    public PlayerConfiguration GetPlayerConfig()
    {
        return playerConfig;
    }
}
