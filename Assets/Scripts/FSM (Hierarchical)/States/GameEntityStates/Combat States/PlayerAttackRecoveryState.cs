using UnityEngine;

public class PlayerAttackRecoveryState : PlayerBaseState
{
    private AttackData _attackData;

    public PlayerAttackRecoveryState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // Функціонал початку стану (Attack recovery)
        _attackData = Ctx.currentAttack;
        Debug.Log("Ми в стані після атаки! Тут ми зможемо продовжити наше комбо " + Ctx.currentAttack);
    }

    public override void UpdateState()
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані ACTION");
        if (intent.Type == IntentType.Attack && intent.IsPressed)
        {
            if (_attackData.chargeTime == 0)
            {
                SwitchState(Factory.AttackRelease());
            }
            else
            {
                SwitchState(Factory.AttackCharge());
            }

            //entity.entityAnimator.PlayAnimation("entity_attack"); // punch when moving
            return;
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }

    // When player didnt press any buttons to continue the combo, he will return to idle state
    public void OnRecoveryDisableExternal()
    {
        Debug.Log("Returning to idle when player doesnt continue combo");
        SwitchState(Factory.Idle());
    }
}
