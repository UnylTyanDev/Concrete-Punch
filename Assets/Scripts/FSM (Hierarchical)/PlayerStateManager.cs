using UnityEngine;

public class PlayerStateManager : MonoBehaviour, IAnimationEventReceiver
{
    public PlayerMovement Movement;
    public EntityAnimatorManager entityAnimator;
    public InputInterpreter inputInterpreter;

    public AttackData firstAttack; // Base attack (charge) from which branches out all other attacks
    public AttackData currentAttack;

    // getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public float chargeMultiplier;
    public HitboxManager hitbox;

    [Header("Context variables")]
    [SerializeField] PlayerBaseState _currentState;
    [SerializeField] PlayerStateFactory _states;
    public Vector2 moveVector = Vector2.zero;

    //public PlayerFreeState FreeState = new PlayerFreeState();
    //public PlayerMoveState MoveState = new PlayerMoveState();
    //public PlayerGrabState GrabState = new PlayerGrabState();
    //public PlayerStunnedState StunnedState = new PlayerStunnedState();
    //public PlayerActionState ActionState = new PlayerActionState();

    // Задаємо початковий стан для сутності. (Нехай буде вільний стан)
    private void Awake()
    {
        _states = new PlayerStateFactory(this); // adding state factory for this context (player state manager)
        _currentState = _states.Idle();
    }

    // Ця функція дивиться який зараз стан у сутності та виконує її Update метод
    void Update()
    {
        //Debug.Log("Оновлюємо стани.");
        _currentState.UpdateStates();
    }

    // Ця функція викликається всередині інших методів для того щоб була можливість переходити в інші стани
    //public void SwitchState(PlayerBaseState newState)
    //{

    //if (newState == null) return;
    //if (_currentState == newState) return; // захист від зайвих переходів

    //_currentState?.ExitState(this);
    //_currentState = newState;
    //_currentState.EnterState(this);
    //}

    public void ReceiveIntent(Intent intent)
    {
        Debug.Log("Intent received! Its: " + intent.Type + " " + intent.MoveVector);
        Debug.Log("We will handle this intent in this state: " + _currentState);
        _currentState.HandleIntent(this, intent);
    }

    /// <summary>
    /// For calling in Animation Events
    /// </summary>
    public void OnHitboxActivate()
    {
        if (_currentState is PlayerAttackReleaseState attackState)
        {
            attackState.ActivateHitboxExternal();
        }
    }

    public void OnHitboxDeactivate()
    {
        if (_currentState is PlayerAttackReleaseState attackState)
        {
            attackState.DeactivateHitboxExternal();
        }
    }

    public void OnRecoveryEnable()
    {
        if (_currentState is PlayerAttackReleaseState attackState)
        {
            Debug.Log("Recovery state called from animation event!");
            attackState.OnRecoveryEnableExternal();
        }
    }

    public void OnRecoveryDisable()
    {
        if (_currentState is PlayerAttackRecoveryState recoveryState)
        {
            Debug.Log("Disable recovery state called from animation event!");
            recoveryState.OnRecoveryDisableExternal();
        }
    }
}
