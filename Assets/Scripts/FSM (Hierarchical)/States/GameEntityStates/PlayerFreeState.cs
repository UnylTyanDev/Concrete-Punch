using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerFreeState : PlayerBaseState
{
    private bool _firstUpdate;

    public PlayerFreeState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Ми ввійшли в стан Free!");
        _firstUpdate = true;
        // Функціонал початку стану
        // Тут сутність входить в вільний режим де він нічим не обмежений,
        // з цього стану він може перейти в: GrabState, MoveState, StunnedState

        if (Ctx.entityAnimator != null) // Приклад
            Ctx.entityAnimator.PlayAnimation("entity_idle");
    }

    public override void UpdateState()
    {
        if (_firstUpdate)
        {
            _firstUpdate = false;
            // When we entering idle state in cause of returning, 
            // we need to request current input that is currently held, 
            // because player can hold a button through states without releasing it, 
            // and therefore cant receive "HandleIntent()"
            Ctx.inputInterpreter.RequestEvaluateInput();
        }
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        Debug.Log("Обробляємо намір сутності в стані FREE намір: " + intent.Type + " " + intent.MoveVector); 

        if (intent.Type == IntentType.Move)
        {
            Debug.Log("Переходимо у Move!");
            SwitchState(Factory.Walk());
            // Передаємо інтент безпосередньо в новий стан, щоб той зміг встановити moveVector
            entity.moveVector = intent.MoveVector;
            //entity.MoveState.HandleIntent(entity, intent);
            return;
        }
    }

    public override void ExitState() 
    {

    }

    public override void InitializeSubState()
    {

    }
}
