using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class EntityMoveState : EntityBaseState
{
    Vector2 moveVector = Vector2.zero;

    public override void EnterState(EntityStateManager entity)
    {
        // Функціонал початку стану
        Animator animator = entity.GetComponent<Animator>(); // Приклад
        if (animator != null)
            animator.Play("entity_move");
    }

    public override void UpdateState(EntityStateManager entity)
    {
        // Функціонал який виконується кожен кадр для цього стану
        if (moveVector.sqrMagnitude > 0.0001f)
        {
            // Здесь вызываем исполнитель — Movement, который просто двигает
            Debug.Log("Рухаємося:" + moveVector);
            //entity.Movement.Move(moveVector);
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
        Debug.Log("Обробляємо намір сутності в стані MOVE");

        if (intent.Type == IntentType.Move)
        {
            moveVector = intent.MoveVector;
            return;
        }

        // Приклад: Під час руху з'явився намір когось вдарити або схопити - FSM дозволяє переходи
        if (intent.Type == IntentType.Grab)
        {
            entity.SwitchState(entity.MoveState);
            // Передаємо цей самий намір в наступний стан щоб він також обработався
            entity.ReceiveIntent(intent);
        }
        else if (intent.Type == IntentType.Attack)
        {
            entity.SwitchState(entity.MoveState);
            entity.ReceiveIntent(intent);
        }
    }

    public override void ExitState(EntityStateManager entity)
    {
        moveVector = Vector2.zero;
    }
}
