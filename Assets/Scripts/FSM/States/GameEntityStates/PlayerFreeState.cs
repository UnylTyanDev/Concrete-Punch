using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerFreeState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager entity)
    {
        // Функціонал початку стану
        // Тут сутність входить в вільний режим де він нічим не обмежений,
        // з цього стану він може перейти в: GrabState, MoveState, StunnedState
 
        if (entity.entityAnimator != null) // Приклад
            entity.entityAnimator.PlayAnimation("entity_idle");
    }

    public override void UpdateState(PlayerStateManager entity)
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані FREE");

        if (intent.Type == IntentType.Move && intent.MoveVector.sqrMagnitude > 0.001f)
        {
            entity.SwitchState(entity.MoveState);
            // Передаємо інтент безпосередньо в новий стан, щоб той зміг встановити moveVector
            entity.MoveState.HandleIntent(entity, intent);
            return;
        }
    }

    public override void ExitState(PlayerStateManager entity) { }
}
