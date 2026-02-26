using UnityEngine;
/// <summary>
/// A state where a the player is the one who is holding someone
/// From this state it can transition to: FreeState
/// </summary>
public class PlayerGrabbedState : PlayerBaseState
{
    private IGrabbable _target;
    private bool _canAttack = true;
    public PlayerGrabbedState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
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
        // Synch the victim transform according to player
        _target.GetTransform().SetParent(Ctx.grabHoldPoint);

    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        // Debug.Log("Processing entity intent in GRABBED state");
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }
}
