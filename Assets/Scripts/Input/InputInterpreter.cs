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

    void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.action.performed += OnMovePerformed;
            moveAction.action.canceled += OnMoveCanceled;
        }

        if (attackAction != null)
            attackAction.action.started += ctx => SendIntent(Intent.Attack());

        if (grabAction != null)
            grabAction.action.started += ctx => SendIntent(Intent.Grab());
    }

    void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.action.performed -= OnMovePerformed;
            moveAction.action.canceled -= OnMoveCanceled;
        }

        if (attackAction != null)
            attackAction.action.started -= ctx => SendIntent(Intent.Attack());

        if (grabAction != null)
            grabAction.action.started -= ctx => SendIntent(Intent.Grab());
    }

    void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        // Надсилаємо намір в FSM сутності, вона сама вирішить чи виконається дія в певному стані
        SendIntent(Intent.Move(v));
    }

    void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        // Нульовий вектор це в нас повна зупинка
        SendIntent(Intent.Move(Vector2.zero));
    }

    void SendIntent(Intent intent)
    {
        if (playerManager != null)
            playerManager.ReceiveIntent(intent);
    }
}
