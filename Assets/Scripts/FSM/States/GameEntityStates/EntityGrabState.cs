using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class EntityGrabState : EntityBaseState
{
    public override void EnterState(EntityStateManager entity)
    {
        // Функціонал початку стану
        // Тут сутність входить в режим захвату, коли її або вона хватає когось.
        // З цього режиму вона може перейти в: StunnedState, ActionState
    }

    public override void UpdateState(EntityStateManager entity)
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(EntityStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані GRAB");
    }

    public override void ExitState(EntityStateManager entity) { }
}
