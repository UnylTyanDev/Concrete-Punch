using UnityEngine;
/// <summary>
/// A state where Enemy is throwing a punch. (Active state of punch attack)
/// From this state it can transition to: RecoveryAttack (Called from unity animation system), FreeState, MoveState, AttackCharge, RunState
/// </summary>
public class EnemyAttackReleaseState : EnemyBaseState
{
    private AttackData _attackData;
    private bool _hitboxActivated;

    public EnemyAttackReleaseState(EnemyStateManager currentContext, EnemyStateFactory EnemyStateFactory) : base(currentContext, EnemyStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // State initialization functionality (Attack release)
        Debug.Log("Ворог в Release state!");
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
        Ctx.hitbox.ActivateDamage(_attackData.baseDamage, Ctx.chargeMultiplier, _attackData.direction, _attackData.knockbackForce);
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

    // This function is called when we want to enter to recovery state, so Enemy could press next buttons to continue the combo
    public void OnRecoveryEnableExternal()
    {
        if (_attackData.nextAttack != null)
        {
            Ctx.currentAttack = _attackData.nextAttack;
            SwitchState(Factory.AttackRecovery());
        }
        else
        {
            // Конец комбо
            Ctx.currentAttack = null;
            SwitchState(Factory.Idle());
        }
    }

    public override void HandleHurtEvent()
    {
        Ctx.entityAnimator.PlayAnimation("entity_hurt");
        SwitchState(Factory.Stunned());
    }
}
