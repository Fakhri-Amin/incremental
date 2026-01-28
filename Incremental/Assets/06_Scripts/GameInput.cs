using System;
using Eggtato.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : PersistentSingleton<GameInput>
{
    public Action<InputAction.CallbackContext> OnInteractStarted;
    public Action<InputAction.CallbackContext> OnInteractCanceled;

    private PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }

    public void SwitchActionMap(string mapName)
    {
        playerInput.SwitchCurrentActionMap(mapName);
    }

    public Vector2 GetMousePosition() => playerInput.actions["Point"].ReadValue<Vector2>();

    public Vector2 GetMoveVector()
    {
        return playerInput.actions["Move"].ReadValue<Vector2>();
    }

    public bool IsSprintPressed()
    {
        return playerInput.actions["Sprint"].IsPressed();
    }

    public bool IsSpacePressed()
    {
        return playerInput.actions["Jump"].IsPressed();
    }

    public bool WasEscapePressed()
    {
        return playerInput.actions["Cancel"].WasPressedThisFrame();
    }

    public bool WasAttackPressed()
    {
        return playerInput.actions["Attack"].WasPressedThisFrame();
    }
}
