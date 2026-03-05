using UnityEngine;
/// <summary>
/// A state where Enemy is in the window of opportunity when he can continue his current combo.
/// From this state it can transition to: FreeState, MoveState, AttackCharge, AttackRelease, RunState
/// </summary>
public class EnemyAttackRecoveryState : EnemyBaseState
{
    private AttackData _attackData;

    public EnemyAttackRecoveryState(EnemyStateManager currentContext, EnemyStateFactory EnemyStateFactory) : base(currentContext, EnemyStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // State initialization functionality (Attack RECOVERY)
        _attackData = Ctx.currentAttack;
        Debug.Log("Ворог в стані після атаки! Він може продовжити комбо " + Ctx.currentAttack);
    }

    public override void UpdateState()
    {
        // Проверяем, какой тип следующей атаки

        if (_attackData == null)
        {
            _attackData = Ctx.currentAttack;
        }

        float chance = 0.7f; // 70% продолжить
        if (Random.value <= chance)
        {
            if (_attackData.chargeTime == 0)
            {
                SwitchState(Factory.AttackRelease());
                return;
            }
            else
            {
                SwitchState(Factory.AttackCharge());
            }
        }
        else
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }

    // When Enemy didnt press any buttons to continue the combo, he will return to idle state
    public void OnRecoveryDisableExternal()
    {
        Debug.Log("Returning to idle when Enemy doesnt continue combo");
        SwitchState(Factory.Idle());
    }

    public override void HandleHurtEvent()
    {
        Ctx.entityAnimator.PlayAnimation("entity_hurt");
        SwitchState(Factory.Stunned());
    }
}
