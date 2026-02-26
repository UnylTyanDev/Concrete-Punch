using UnityEngine;
/// <summary>
/// A state where player is throwing a punch. (Active state of punch attack)
/// From this state it can transition to: RecoveryAttack (Called from unity animation system), FreeState, MoveState, AttackCharge, RunState
/// </summary>
public class PlayerAttackReleaseState : PlayerBaseState
{
    private AttackData _attackData;
    private bool _hitboxActivated;

    public PlayerAttackReleaseState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // State initialization functionality (Attack release)
        Debug.Log("Ми в Release state!");
        Ctx.Movement.SetMoveInput(Vector2.zero); // We can add a little force when player throws a punch (Attack release)
        _attackData = Ctx.currentAttack;

        if (_attackData == null) // If there is no next attack, that means the end of the combo attack
        {
            SwitchState(Factory.Idle());

            return;
        }

        Ctx.entityAnimator.PlayAnimation(_attackData.animationName);

        _hitboxActivated = false;
    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
    }

    private void ActivateHitbox()
    {
        Ctx.hitbox.Activate(_attackData.baseDamage, Ctx.chargeMultiplier, _attackData.direction, _attackData.knockbackForce);
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        // Debug.Log("Processing entity intent in ACTION state");
    }

    public override void ExitState()
    {
        Ctx.hitbox.Deactivate();
    }

    public override void InitializeSubState()
    {

    }

    /// <summary>
    /// Using in case when we want activate hitbox from Unity Animation Events (In specific timing in animation)
    /// </summary>
    public void ActivateHitboxExternal()
    {
        if (!_hitboxActivated)
        {
            ActivateHitbox();
            _hitboxActivated = true;
        }
    }

    public void DeactivateHitboxExternal()
    {
        if (_hitboxActivated)
        {
            Ctx.hitbox.Deactivate();
            _hitboxActivated = false;
        }
    }

    // This function is called when we want to enter to recovery state, so player could press next buttons to continue the combo
    public void OnRecoveryEnableExternal()
    {
        Ctx.currentAttack = _attackData.nextAttack;
        SwitchState(Factory.AttackRecovery());
    }
}
