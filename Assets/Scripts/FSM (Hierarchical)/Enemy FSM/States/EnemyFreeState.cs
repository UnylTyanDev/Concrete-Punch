using Unity.Android.Gradle.Manifest;
using UnityEngine;

/// <summary>
/// A state of free mode where entity is unrestricted, and can enter majority of states
/// from this state it can transition to: GrabState, MoveState, AttackCharge, RunState
/// </summary>
public class EnemyFreeState : EnemyBaseState
{
    private float _checkTimer;
    private float _checkInterval = 1f;

    public EnemyFreeState(EnemyStateManager currentContext, EnemyStateFactory EnemyStateFactory) : base(currentContext, EnemyStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // State start functionality

        Debug.Log("Ворог в стані Free!");

        if (Ctx.entityAnimator != null) // Приклад
            Ctx.entityAnimator.PlayAnimation("entity_idle");
    }

    public override void UpdateState()
    {
        _checkTimer += Time.deltaTime;

        if (_checkTimer < _checkInterval) return;
        _checkTimer = 0f;

        if (Ctx.playerTransform == null) return;

        float radiusSqr = Ctx.detectionRadius * Ctx.detectionRadius;

        Vector3 diff = Ctx.playerTransform.position - Ctx.enemyTransform.position;
        diff.y = 0;

        if (diff.sqrMagnitude <= radiusSqr)
        {
            Debug.Log("Гравець в радіусі, ворог починає рухатись");
            SwitchState(Factory.Walk());
        }
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {

    }

    public override void HandleHurtEvent()
    {
        Ctx.entityAnimator.PlayAnimation("entity_hurt");
        SwitchState(Factory.Stunned());
    }
}
