using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerMovement Movement;
    public EntityAnimatorManager entityAnimator;
    PlayerBaseState currentState;

    public PlayerFreeState FreeState = new PlayerFreeState();
    public PlayerMoveState MoveState = new PlayerMoveState();
    public PlayerGrabState GrabState = new PlayerGrabState();
    public PlayerStunnedState StunnedState = new PlayerStunnedState();
    public PlayerActionState ActionState = new PlayerActionState();

    // Задаємо початковий стан для сутності. (Нехай буде вільний стан)
    void Start()
    {
        currentState = FreeState;
        currentState.EnterState(this);
    }

    // Ця функція дивиться який зараз стан у сутності та виконує її Update метод
    void Update()
    {
        currentState.UpdateState(this);
    }

    // Ця функція викликається всередині інших методів для того щоб була можливість переходити в інші стани
    public void SwitchState(PlayerBaseState newState)
    {
        if (newState == null) return;
        if (currentState == newState) return; // захист від зайвих переходів

        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void ReceiveIntent(Intent intent)
    {
        currentState.HandleIntent(this, intent);
    }
}
