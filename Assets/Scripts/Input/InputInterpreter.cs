using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputInterpreter : MonoBehaviour
{
    // Тут ми через інспектор прив'язуємо дії з Unity Input System
    public InputActionReference moveAction;
    public InputActionReference attackAction;
    public InputActionReference grabAction;

    // Тут в нас посилання на Finite State Machine сутності
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

    // ---- НОВОЕ: внешний запрос текущего ввода ----
    // Вызывается FSM (через PlayerStateManager), когда нужно "услышать" удерживаемый ввод
    public void RequestEvaluateInput()
    {
        Debug.Log("REQUESTING CURRENT INPUT");
        // Сначала буферные/приоритетные интенты (если есть) — можно расширять

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

        // Если ничего нет — можно послать "Idle" или ничего не делать
        // SendIntent(Intent.Idle()); // опционально
    }
}
