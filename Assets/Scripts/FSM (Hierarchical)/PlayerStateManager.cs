using UnityEngine;
/// <summary>
/// The context of the player entity. Holds all the information that is relevant to a player
/// </summary>
public class PlayerStateManager : MonoBehaviour, IAnimationEventReceiver
{
    public PlayerMovement Movement;
    public EntityAnimatorManager entityAnimator;
    public InputInterpreter inputInterpreter;

    public AttackData firstAttack; // Base attack (charge) from which branches out all other attacks
    public AttackData currentAttack;
    public IGrabbable grabbedTarget;
    public Transform grabHoldPoint;

    // getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public float chargeMultiplier;
    public HitboxManager hitbox; // for attacks and grab

    [Header("Context variables")]
    [SerializeField] PlayerBaseState _currentState;
    [SerializeField] PlayerStateFactory _states;
    public Vector2 moveVector = Vector2.zero;

    // Setting the starting state for a player
    private void Awake()
    {
        _states = new PlayerStateFactory(this); // adding state factory for this context (player state manager)
        _currentState = _states.Idle();
    }

    /// <summary>
    /// This function checks the entity's current state and executes its Update method
    /// </summary>
    void Update()
    {
        //Debug.Log("Оновлюємо стани.");
        _currentState.UpdateStates();
    }

    /// <summary>
    /// A function, which purpose is to receive the intent given from input inprerpreter.
    /// Sends to the current state this intent, and its being handled in diferent states diferently
    /// </summary>
    /// <param name="intent"></param>
    public void ReceiveIntent(Intent intent)
    {
        Debug.Log("Intent received! Its: " + intent.Type + " " + intent.MoveVector);
        Debug.Log("We will handle this intent in this state: " + _currentState);
        _currentState.HandleIntent(this, intent);
    }

    /// <summary>
    /// For activating the hitbox from outside by Animation Events. (FSM Function)
    /// </summary>
    public void OnHitboxActivate()
    {
        if (_currentState is PlayerAttackReleaseState attackState)
        {
            attackState.ActivateHitboxExternal();
        }
    }
    /// <summary>
    /// For deactivating the hitbox from outside by Animation Events. (FSM Function)
    /// </summary>
    public void OnHitboxDeactivate()
    {
        if (_currentState is PlayerAttackReleaseState attackState)
        {
            attackState.DeactivateHitboxExternal();
        }
    }
    /// <summary>
    /// For entering the recovery state from outside call by Animation Events. (FSM Function)
    /// </summary>
    public void OnRecoveryEnable()
    {
        if (_currentState is PlayerAttackReleaseState attackState)
        {
            Debug.Log("Recovery state called from animation event!");
            attackState.OnRecoveryEnableExternal();
        }
    }
    /// <summary>
    /// For exit the recovery state from outside call by Animation Events. (FSM Function)
    /// </summary>
    public void OnRecoveryDisable()
    {
        if (_currentState is PlayerAttackRecoveryState recoveryState)
        {
            Debug.Log("Disable recovery state called from animation event!");
            recoveryState.OnRecoveryDisableExternal();
        }
    }
    /// <summary>
    /// When we
    /// </summary>
    public void OnSuccesGrab()
    {
        if (_currentState is PlayerGrabState grabState)
        {
            grabState.OnGrabCompleted();
        }
    }
}
