using UnityEngine;

/// <summary>
/// A state where Enemy is stunned by enemy attack, and playing hurt animation
/// From this state it can transition to: FreeState
/// </summary>
public class EnemyStunnedState : EnemyBaseState
{
    float stunTimer;

    public EnemyStunnedState(EnemyStateManager currentContext, EnemyStateFactory EnemyStateFactory)
        : base(currentContext, EnemyStateFactory) 
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        stunTimer = Ctx.stunDuration;
        Ctx.entityAnimator.PlayAnimation("entity_hurt");
    }

    public override void UpdateState()
    {
        stunTimer -= Time.deltaTime;

        if (stunTimer <= 0f)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void HandleHurtEvent()
    {
        // if entity got hit in stun state, its resets stun timer
        Ctx.entityAnimator.PlayAnimation("entity_hurt");
        stunTimer = Ctx.stunDuration;
    }
}