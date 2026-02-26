using Unity.Android.Gradle.Manifest;
using UnityEngine;

/// <summary>
/// A state of free mode where entity is unrestricted, and can enter majority of states
/// from this state it can transition to: GrabState, MoveState, AttackCharge, RunState
/// </summary>
public class PlayerFreeState : PlayerBaseState
{
    private bool _firstUpdate;

    public PlayerFreeState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // State start functionality

        Debug.Log("Ми ввійшли в стан Free!");
        _firstUpdate = true;

        if (Ctx.entityAnimator != null) // Приклад
            Ctx.entityAnimator.PlayAnimation("entity_idle");
    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
        if (_firstUpdate)
        {
            _firstUpdate = false;
            // When we entering idle state in cause of returning, 
            // we need to request current input that is currently held, 
            // because player can hold a button through states without releasing it, 
            // and therefore cant receive "HandleIntent()"
            Ctx.inputInterpreter.RequestEvaluateInput();
        }
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        Debug.Log("Обробляємо намір сутності в стані FREE намір: " + intent.Type + " " + intent.MoveVector); 

        if (intent.Type == IntentType.Move)
        {
            Debug.Log("Переходимо у Move!");
            SwitchState(Factory.Walk());
            // Передаємо інтент безпосередньо в новий стан, щоб той зміг встановити moveVector
            entity.moveVector = intent.MoveVector;
            //entity.MoveState.HandleIntent(entity, intent);
            return;
        }
    }

    public override void ExitState() 
    {

    }

    public override void InitializeSubState()
    {

    }
}
