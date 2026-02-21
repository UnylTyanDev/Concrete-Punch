using UnityEngine;

public interface IAnimationEventReceiver
{
    void OnHitboxActivate();
    void OnHitboxDeactivate();
    void OnRecoveryEnable();
    void OnRecoveryDisable();
}
