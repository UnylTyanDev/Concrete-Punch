using UnityEngine;

/// <summary>
/// A state where player is stunned by enemy attack, and playing hurt animation
/// From this state it can transition to: FreeState
/// </summary>
public class PlayerStunnedState : PlayerBaseState
{
    float stunTimer;

    public PlayerStunnedState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.LogWarning("Гравець в стані!");
        stunTimer = Ctx.stunDuration;

        // играем анимацию удара
        Ctx.entityAnimator.PlayAnimation("entity_hurt");

        // останавливаем движение
        Ctx.moveVector = Vector2.zero;
    }

    public override void UpdateState()
    {
        Debug.Log("Player stun: " + stunTimer);
        stunTimer -= Time.deltaTime;

        if (stunTimer <= 0f)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Во время стана игрок не может выполнять действия
        // Просто игнорируем интенты
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void HandleHurtEvent()
    {
        // если ударили снова во время стана — просто обновляем таймер
        stunTimer = Ctx.stunDuration;
    }
}