using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerStunnedState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager entity)
    {
        // Функціонал початку стану
    }

    public override void UpdateState(PlayerStateManager entity)
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані STUNNED");
    }

    public override void ExitState(PlayerStateManager entity) { }
}
