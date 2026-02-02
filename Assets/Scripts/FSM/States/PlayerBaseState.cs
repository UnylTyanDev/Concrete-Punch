using Unity.Android.Gradle.Manifest;
using UnityEngine;

public abstract class PlayerBaseState
{
    // Функція котра виконується при вході в стан
    public abstract void EnterState(PlayerStateManager entity);

    // Функція котра обробляє кожну секунду функціонал певного стану
    public abstract void UpdateState(PlayerStateManager entity);

    // Функція яка виконується при виході зі стану (Cleanup)
    public virtual void ExitState(PlayerStateManager entity) { }

    // Обробляє наміри сутності які були викликані з менеджеру станів
    public virtual void HandleIntent(PlayerStateManager entity, Intent intent) { }
}
