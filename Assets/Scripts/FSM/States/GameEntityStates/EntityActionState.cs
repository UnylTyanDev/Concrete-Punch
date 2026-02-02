using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class EntityActionState : EntityBaseState
{
    public override void EnterState(EntityStateManager entity)
    {
        // Функціонал початку стану
        entity.animator.Play("entity_attack");
    }

    public override void UpdateState(EntityStateManager entity)
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(EntityStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані ACTION");
    }

    public override void ExitState(EntityStateManager entity) { }
}
