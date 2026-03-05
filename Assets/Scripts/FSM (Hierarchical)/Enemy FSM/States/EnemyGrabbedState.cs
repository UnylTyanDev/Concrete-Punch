using UnityEngine;
/// <summary>
/// A state where a the Enemy is the one who is holding someone
/// From this state it can transition to: FreeState
/// </summary>
public class EnemyGrabbedState : EnemyBaseState
{
    private IGrabbable _target;
    //private bool _canAttack = true;
    public EnemyGrabbedState(EnemyStateManager currentContext, EnemyStateFactory EnemyStateFactory) : base(currentContext, EnemyStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // Function that is executed when entering the state
        _target = Ctx.grabbedTarget;
        if (_target == null)
        {
            SwitchState(Factory.Idle());
            return;
        }
        // Launch holding (grab) idle animation
        Ctx.entityAnimator.PlayAnimation("grab_hold");
        // Synch the victim transform according to Enemy
        _target.GetTransform().SetParent(Ctx.enemyTransform);

    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }
    public override void HandleHurtEvent()
    {
        Ctx.entityAnimator.PlayAnimation("entity_hurt");
        SwitchState(Factory.Stunned());
    }
}
