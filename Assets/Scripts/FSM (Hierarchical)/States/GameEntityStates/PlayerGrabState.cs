using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerGrabState : PlayerBaseState
{
    public PlayerGrabState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        // Функціонал початку стану
        // Тут сутність входить в режим захвату, коли її або вона хватає когось.
        // З цього режиму вона може перейти в: StunnedState, ActionState

    }

    public override void UpdateState()
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані GRAB");
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }
}
