using UnityEngine;

/// <summary>
/// A state where player is trying to catch someone for grab state
/// From this state it can transition to: GrabbedState, FreeState
/// </summary>
public class PlayerGrabState : PlayerBaseState
{
    private float _grabRange;
    private LayerMask _grabbableLayer;

    public PlayerGrabState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // State start functionality
        // Here the entity enters grab mode, either grabbing someone or being grabbed.
        // From this mode, it can transition to: StunnedState, ActionState
        Debug.Log("Trying to grab...");
        Ctx.moveVector = Vector2.zero; // Stops player when doing action
        Ctx.entityAnimator.PlayAnimation("entity_grab");
    }

    public override void UpdateState()
    {
        // Functionality executed every frame for this state
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        // Debug.Log("Processing entity intent in GRAB state");
    }

    public override void ExitState()
    {
        Ctx.hitbox.Deactivate();
    }

    public override void InitializeSubState()
    {

    }
    /// <summary>
    /// Go to grabbed state when we grabbed someone (Calls from Unity Animation Events)
    /// </summary>
    public void GrabCheckExternal()
    {
        if (Ctx.hitbox.TryGrab(Ctx.transform, out IGrabbable target))
        {
            Debug.Log("У нас є ціль яку можна схвопити. Схоплюємо");
            Ctx.grabbedTarget = target;
            // Here we can play animation of succesfull grab
        }
    }

    public void GrabDisableExternal()
    {
        Debug.Log("Нікого не найшли щоб схопити, переходимо у Free виходячи з Grab");
        SwitchState(Factory.Idle());
    }

    public override void HandleHurtEvent()
    {
        Ctx.entityAnimator.PlayAnimation("entity_hurt");
        SwitchState(Factory.Stunned());
    }

}
