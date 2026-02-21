using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Debug.Log("Ми ввійшли в стан Move!");

        // Функціонал початку стану 
        if (Ctx.entityAnimator != null)
            Ctx.entityAnimator.PlayAnimation("entity_move");// Приклад
    }

    public override void UpdateState()
    {
        // Функціонал який виконується кожен кадр для цього стану
        //Debug.Log("Оновлюємо переміщення " + Ctx.moveVector.sqrMagnitude);
        
        if (Ctx.moveVector.sqrMagnitude > 0.0001f)
        {
            // Здесь вызываем исполнитель — Movement, который просто двигает
            //Debug.Log("Рухаємося:" + Ctx.moveVector);
            Ctx.Movement.SetMoveInput(Ctx.moveVector);
        }
        else
        {
            // Нет ввода — возвращаемся в свободный
            SwitchState(Factory.Idle());
        }
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані MOVE");

        if (intent.Type == IntentType.Move)
        {
            //Debug.Log("Присвоюємо вектор напряму руху до самого контекста. Вектор: " + intent.MoveVector);
            entity.moveVector = intent.MoveVector;
            return;
        }

        // Якщо потрібно перейти в інші стани — переключаємося на їхні інстанси,
        // а не на MoveState (і не викликаємо ExitState вручну).
        if (intent.Type == IntentType.Grab)
        {
            SwitchState(Factory.Walk());
            // якщо потрібно, можно передати інтент: entity.GrabState.HandleIntent(entity, intent);
            return;
        }
        else if (intent.Type == IntentType.Attack && intent.IsPressed)
        {
            Ctx.currentAttack = Ctx.firstAttack;
            SwitchState(Factory.AttackCharge());
            //entity.entityAnimator.PlayAnimation("entity_attack"); // punch when moving
            return;
        }
    }

    public override void ExitState()
    {
        //entity.moveVector = Vector2.zero;
    }

    public override void InitializeSubState()
    {
        
    }
}
