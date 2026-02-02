using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class EntityMoveState : EntityBaseState
{
    Vector2 moveVector = Vector2.zero;

    public override void EnterState(EntityStateManager entity)
    {
        // Функціонал початку стану 
        if (entity.animator != null)
            entity.animator.Play("entity_move");// Приклад
    }

    public override void UpdateState(EntityStateManager entity)
    {
        // Функціонал який виконується кожен кадр для цього стану
        if (moveVector.sqrMagnitude > 0.0001f)
        {
            // Здесь вызываем исполнитель — Movement, который просто двигает
            Debug.Log("Рухаємося:" + moveVector);
            entity.Movement.SetMoveInput(moveVector);
        }
        else
        {
            // Нет ввода — возвращаемся в свободный
            entity.SwitchState(entity.FreeState);
        }
    }

    public override void HandleIntent(EntityStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані MOVE");

        if (intent.Type == IntentType.Move)
        {
            moveVector = intent.MoveVector;
            return;
        }

        // Якщо потрібно перейти в інші стани — переключаємося на їхні інстанси,
        // а не на MoveState (і не викликаємо ExitState вручну).
        if (intent.Type == IntentType.Grab)
        {
            entity.SwitchState(entity.GrabState);
                                                  // якщо потрібно, можно передати інтент: entity.GrabState.HandleIntent(entity, intent);
            return;
        }
        else if (intent.Type == IntentType.Attack)
        {
            entity.SwitchState(entity.ActionState);
            return;
        }
    }

    public override void ExitState(EntityStateManager entity)
    {
        moveVector = Vector2.zero;
    }
}
