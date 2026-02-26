using UnityEngine;

/// <summary>
/// A state where player is trying to catch someone for grab state
/// From this state it can transition to: GrabbedState, FreeState
/// </summary>
public class PlayerGrabState : PlayerBaseState
{
    private float _grabRange;
    private LayerMask _grabbableLayer;

    public PlayerGrabState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        // State start functionality
        // Here the entity enters grab mode, either grabbing someone or being grabbed.
        // From this mode, it can transition to: StunnedState, ActionState
        Debug.Log("Trying to grab...");
        Collider[] hits = Physics.OverlapSphere(Ctx.transform.position, _grabRange, _grabbableLayer);
        IGrabbable target = null;
        foreach (var hit in hits)
        {
            var g = hit.GetComponent<IGrabbable>();
            if (g != null && g.CanBeGrabbed)
            {
                target = g;
                break;
            }
        }
        if (target != null)
        {
            // Succesfull grab
            Ctx.grabbedTarget = target;
            target.OnGrabbed(Ctx.transform);
            Ctx.entityAnimator.PlayAnimation("entity_grab");
        }
        else
        {
            SwitchState(Factory.Idle());
        }

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

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }
    /// <summary>
    /// Go to grabbed state when we grabbed someone (Calls from Unity Animation Events)
    /// </summary>
    public void OnGrabCompleted()
    {
        SwitchState(Factory.Grabbed());
    }
}
