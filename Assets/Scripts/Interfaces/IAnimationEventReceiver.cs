using UnityEngine;
/// <summary>
/// Interface, that is indicates the ability of a class to have certain functions.
/// And to be more precise, in this case, this interface accepts the animation events
/// </summary>
public interface IAnimationEventReceiver
{
    void OnHitboxActivate();
    void OnHitboxDeactivate();
    void OnRecoveryEnable();
    void OnRecoveryDisable();
    void OnSuccesGrab();
}
