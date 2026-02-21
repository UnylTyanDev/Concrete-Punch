using UnityEngine;

/// <summary>
/// The only purpuse of this class is calling functions when Unity Events tolds them to 
/// (Because when animator component is on child object, when the parent object has the class we want to call)
/// (And because of that Unity Animation Events cant reach)
/// </summary>
public class AnimationEventForwarder : MonoBehaviour
{
    [SerializeField] private MonoBehaviour receiver;

    private IAnimationEventReceiver eventReceiver;

    private void Awake()
    {
        eventReceiver = receiver as IAnimationEventReceiver;

        if (eventReceiver == null)
        {
            Debug.LogError("Receiver does not implement IAnimationEventReceiver");
        }
    }

    public void EnableHitbox() => eventReceiver?.OnHitboxActivate();

    public void DisableHitbox() => eventReceiver?.OnHitboxDeactivate();

    public void EnableRecovery() => eventReceiver?.OnRecoveryEnable();

    public void DisableRecovery() => eventReceiver?.OnRecoveryDisable();
}
