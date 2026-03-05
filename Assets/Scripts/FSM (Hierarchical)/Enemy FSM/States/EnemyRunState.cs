
/// <summary>
/// A state where it is changing its transform by moving in space but faster.
/// From this state it can transition to: GrabState, FreeState, MoveState, AttackCharge, RunState
/// </summary>
public class EnemyRunState : EnemyBaseState
{
    public EnemyRunState(EnemyStateManager currentContext, EnemyStateFactory EnemyStateFactory) : base(currentContext, EnemyStateFactory) { }
    public override void EnterState()
    {
        // Function that is executed when entering the state
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
