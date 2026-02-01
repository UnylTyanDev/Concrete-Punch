using Unity.Android.Gradle.Manifest;
using UnityEngine;

public abstract class EntityBaseState
{
    // Функція котра виконується при вході в стан
    public abstract void EnterState(EntityStateManager entity);

    // Функція котра обробляє кожну секунду функціонал певного стану
    public abstract void UpdateState(EntityStateManager entity);

    // Функція яка виконується при виході зі стану (Cleanup)
    public virtual void ExitState(EntityStateManager entity) { }

    // Обробляє наміри сутності які були викликані з менеджеру станів
    public virtual void HandleIntent(EntityStateManager entity, Intent intent) { }
}
