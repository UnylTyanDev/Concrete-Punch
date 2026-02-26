
/// <summary>
/// A state where it is changing its transform by moving in space but faster.
/// From this state it can transition to: GrabState, FreeState, MoveState, AttackCharge, RunState
/// </summary>
public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        // Function that is executed when entering the state
    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        // Debug.Log("Processing entity intent in RUN state");
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }
}
