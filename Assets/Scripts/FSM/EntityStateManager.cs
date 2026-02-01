using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class EntityStateManager : MonoBehaviour
{
    EntityBaseState currentState;

    public EntityFreeState FreeState = new EntityFreeState();
    public EntityMoveState MoveState = new EntityMoveState();
    public EntityGrabState GrabState = new EntityGrabState();
    public EntityStunnedState StunnedState = new EntityStunnedState();
    public EntityActionState ActionState = new EntityActionState();

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
    public void SwitchState(EntityBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void ReceiveIntent(Intent intent)
    {
        currentState.HandleIntent(this, intent);
    }
}
