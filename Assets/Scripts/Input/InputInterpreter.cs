using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class, that reads the Unity Input System signals and transforms them into intents, which can be handled inside of the states (FSM) differently.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputInterpreter : MonoBehaviour
{
    // Here we link the action to the Unity Input System through the inspector
    public InputActionReference moveAction;
    public InputActionReference attackAction;
    public InputActionReference grabAction;

    // Here we have a link on the Finite State Machine of the entity
    public PlayerStateManager playerManager;

    // Current input capture
    private Vector2 _currentMove = Vector2.zero;
    private bool _attackHeld = false;
    private bool _grabHeld = false;
    private float _deadzone = 0.1f;

    void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.action.performed += OnMovePerformed;
            moveAction.action.canceled += OnMoveCanceled;
        }

        if (attackAction != null)
        {
            attackAction.action.started += ctx => { _attackHeld = true; SendIntent(Intent.Attack(true)); };
            attackAction.action.canceled += ctx => { _attackHeld = false; SendIntent(Intent.Attack(false)); };
        }

        if (grabAction != null)
            grabAction.action.started += ctx => { _grabHeld = true; SendIntent(Intent.Grab()); };
    }

    void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.action.performed -= OnMovePerformed;
            moveAction.action.canceled -= OnMoveCanceled;
        }

        if (attackAction != null)
        {
            attackAction.action.started -= ctx => { _attackHeld = true; SendIntent(Intent.Attack(true)); };
            attackAction.action.canceled -= ctx => { _attackHeld = false; SendIntent(Intent.Attack(false)); };
        }

        if (grabAction != null)
            grabAction.action.started -= ctx => { _grabHeld = true; SendIntent(Intent.Grab()); };
    }

    void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        _currentMove = ctx.ReadValue<Vector2>();
        SendIntent(Intent.Move(_currentMove));
    }

    void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        _currentMove = Vector2.zero;
        SendIntent(Intent.Move(Vector2.zero));
    }

    void SendIntent(Intent intent)
    {
        Debug.Log("Sending intent: " + intent);
        if (playerManager != null)
            playerManager.ReceiveIntent(intent);
    }

    // ---- NEW: external request for current input ----
    // Called by the FSM (via PlayerStateManager) when it needs to "read" the held input
    public void RequestEvaluateInput()
    {
        Debug.Log("REQUESTING CURRENT INPUT");
        // First, buffered/priority intents (if any) — can be extended

        if (_currentMove.sqrMagnitude > _deadzone * _deadzone)
        {
            Debug.Log("Sending because of request: MOVEMENT " + _currentMove);
            SendIntent(Intent.Move(_currentMove));
            return;
        }

        if (_attackHeld)
        {
            Debug.Log("Sending because of request: Attack");
            SendIntent(Intent.Attack(true));
            return;
        }

        if (_grabHeld)
        {
            Debug.Log("Sending because of request: Grab");
            SendIntent(Intent.Grab());
            return;
        }

        // If there’s nothing — you can send "Idle" or do nothing
        // SendIntent(Intent.Idle()); // optional
    }
}
