using UnityEngine;

/// <summary>
/// A state where it is changing its transform by moving in space.
/// From this state it can transition to: GrabState, FreeState, AttackCharge, RunState
/// </summary>
public class EnemyMoveState : EnemyBaseState
{
    private float updateTimer;
    private float updateInterval = 0.5f; // обновлять путь каждые 0.2 секунды

    private float attackCheckTimer;
    private float attackCheckInterval = 0.5f; // проверяем дистанцию для атаки раз в 0.5 сек
    private float attackRange = 2f; // расстояние, на котором враг начинает комбо

    public EnemyMoveState(EnemyStateManager currentContext, EnemyStateFactory EnemyStateFactory) : base(currentContext, EnemyStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        // Function that is executed when entering the state
        Debug.Log("Ворог в стані Move!");
        if (Ctx.entityAnimator != null)
            Ctx.entityAnimator.PlayAnimation("entity_move");

        // Первый раз ставим цель
        if (Ctx.playerTransform != null)
            Ctx.agent.SetDestination(Ctx.playerTransform.position);
    }

    public override void UpdateState()
    {
        if (Ctx.playerTransform == null) return;

        // Обновление пути каждые updateInterval
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            updateTimer = 0f;
            Ctx.agent.SetDestination(Ctx.playerTransform.position);
        }

        // Проверка, достигли ли мы позиции (можно для Idle или AttackState)
        if (!Ctx.agent.pathPending && Ctx.agent.remainingDistance <= Ctx.agent.stoppingDistance)
        {
            if (Ctx.agent.velocity.sqrMagnitude == 0f)
            {
                // Если игрок далеко — Idle
                if ((Ctx.playerTransform.position - Ctx.transform.position).sqrMagnitude > attackRange * attackRange)
                    SwitchState(Factory.Idle());
            }
        }

        // Проверка для запуска комбо атак
        attackCheckTimer += Time.deltaTime;
        if (attackCheckTimer >= attackCheckInterval)
        {
            attackCheckTimer = 0f;
            float sqrDist = (Ctx.playerTransform.position - Ctx.transform.position).sqrMagnitude;
            if (sqrDist <= attackRange * attackRange)
            {
                // Запускаем комбо через AttackChargeState
                Ctx.currentAttack = Ctx.firstAttack; // корневой удар комбо
                SwitchState(Factory.AttackCharge());
            }
        }
    }

    public override void ExitState()
    {
        //entity.moveVector = Vector2.zero;
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
