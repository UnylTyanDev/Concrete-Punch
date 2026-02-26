
/// <summary>
/// A state where player is stunned by enemy attack, and playing hurt animation
/// From this state it can transition to: FreeState
/// </summary>
public class PlayerStunnedState : PlayerBaseState
{
    public PlayerStunnedState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        // Функціонал початку стану
    }

    public override void UpdateState()
    {
        // Функціонал який виконується кожен кадр для цього стану
    }

    public override void HandleIntent(PlayerStateManager entity, Intent intent)
    {
        // Тут визначається намір сутності, та чи виконається сама дія в залежності від стану
        //Debug.Log("Обробляємо намір сутності в стані STUNNED");
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {

    }
}
