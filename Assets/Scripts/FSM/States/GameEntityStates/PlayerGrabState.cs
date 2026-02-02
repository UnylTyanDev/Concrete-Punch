using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerGrabState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager entity)
    {
        // Функціонал початку стану
        // Тут сутність входить в режим захвату, коли її або вона хватає когось.
        // З цього режиму вона може перейти в: StunnedState, ActionState

    }

    public override void UpdateState(PlayerStateManager entity)
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані GRAB");
    }

    public override void ExitState(PlayerStateManager entity) { }
}
