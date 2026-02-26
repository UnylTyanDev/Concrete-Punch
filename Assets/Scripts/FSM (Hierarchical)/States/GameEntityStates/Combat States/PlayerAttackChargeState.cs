using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

/// <summary>
/// A state where player is currently holding the attack button to prepare for a powerful punch.
/// From this state it can transition to: AttackCharge
/// </summary>
public class PlayerAttackChargeState : PlayerBaseState
{
    private float _holdTime;
    private AttackData _attackData;
    public PlayerAttackChargeState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        IsRootState = true; // Cant run at the same time with any other states
    }

    public override void EnterState()
    {
        // State initialization functionality (Attack CHARGE)

        //Debug.Log($"[ChargeState] currentAttack == {Ctx.currentAttack} | entityAnimator == {Ctx.entityAnimator}");
        _attackData = Ctx.currentAttack;
        //Debug.Log($"Animation name: {_attackData.animationName}");
        Ctx.moveVector = Vector2.zero; // Stops player when doing action (Attack charge)
        Ctx.entityAnimator.PlayAnimation(_attackData.animationName);
    }

    public override void UpdateState()
    {
        // Here we are proccesing how long is button being pressed in AttackCharge state
        _holdTime += Time.deltaTime;
        Debug.Log("Charging! " + _holdTime);
        // Multiplier increases liniear from 1 to MAX (For example, 3)
        float t = Mathf.Clamp01(_holdTime / _attackData.chargeTime);
        Ctx.chargeMultiplier = 1f + t * (_attackData.damageMultiplierPerSecond * _attackData.chargeTime);

        if (_holdTime >= _attackData.chargeTime)
        {
            Debug.Log("Release attack!");
            SwitchToRelease();
        }
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Here the entity's intent is determined, and whether the action will be executed depending on the state
        // Debug.Log("Processing entity intent in Attack charge state");
        if (intent.Type == IntentType.Attack && !intent.IsPressed)
        {
            SwitchToRelease();
        }
    }

    private void SwitchToRelease()
    {
        // Its function where we can do something when player releases button when he was in ChargeAttack state
        // Here we giving the control further. We can create a new state (AttackRelease)
        Ctx.currentAttack = _attackData.nextAttack;
        SwitchState(Factory.AttackRelease());
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }
}
