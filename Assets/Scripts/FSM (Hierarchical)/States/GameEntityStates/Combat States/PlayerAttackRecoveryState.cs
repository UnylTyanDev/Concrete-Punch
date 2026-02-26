using UnityEngine;
/// <summary>
/// A state where player is in the window of opportunity when he can continue his current combo.
/// From this state it can transition to: FreeState, MoveState, AttackCharge, AttackRelease, RunState
/// </summary>
public class PlayerAttackRecoveryState : PlayerBaseState
{
    private AttackData _attackData;

    public PlayerAttackRecoveryState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // State initialization functionality (Attack RECOVERY)
        _attackData = Ctx.currentAttack;
        Debug.Log("Ми в стані після атаки! Тут ми зможемо продовжити наше комбо " + Ctx.currentAttack);
    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        // Debug.Log("Processing entity intent in Recovery state");
        if (intent.Type == IntentType.Attack && intent.IsPressed)
        {
            if (_attackData.chargeTime == 0)
            {
                SwitchState(Factory.AttackRelease());
            }
            else
            {
                SwitchState(Factory.AttackCharge());
            }

            //entity.entityAnimator.PlayAnimation("entity_attack"); // punch when moving
            return;
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }

    // When player didnt press any buttons to continue the combo, he will return to idle state
    public void OnRecoveryDisableExternal()
    {
        Debug.Log("Returning to idle when player doesnt continue combo");
        SwitchState(Factory.Idle());
    }
}
