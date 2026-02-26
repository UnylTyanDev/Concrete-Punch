using UnityEngine;

/// <summary>
/// A state where it is changing its transform by moving in space.
/// From this state it can transition to: GrabState, FreeState, AttackCharge, RunState
/// </summary>
public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        // Function that is executed when entering the state
        Debug.Log("Ми ввійшли в стан Move!");
        if (Ctx.entityAnimator != null)
            Ctx.entityAnimator.PlayAnimation("entity_move");// Приклад
    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
        //Debug.Log("Оновлюємо переміщення " + Ctx.moveVector.sqrMagnitude);

        if (Ctx.moveVector.sqrMagnitude > 0.0001f)
        {
            // Here we call the executor — Movement, which simply moves
            //Debug.Log("Рухаємося:" + Ctx.moveVector);
            Ctx.Movement.SetMoveInput(Ctx.moveVector);
        }
        else
        {
            // No input - returning to idle
            SwitchState(Factory.Idle());
        }
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        //Debug.Log("Обробляємо намір сутності в стані MOVE");

        if (intent.Type == IntentType.Move)
        {
            //Debug.Log("Присвоюємо вектор напряму руху до самого контекста. Вектор: " + intent.MoveVector);
            entity.moveVector = intent.MoveVector;
            return;
        }

        // If needed to switch to other states — switch to their instances,
        // not to MoveState (and do not call ExitState manually)
        if (intent.Type == IntentType.Grab)
        {
            SwitchState(Factory.Walk());
            // If needed, you can pass the intent: entity.GrabState.HandleIntent(entity, intent);
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
