using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// The context of the Enemy entity. Holds all the information that is relevant to a Enemy
/// </summary>
public class EnemyStateManager : MonoBehaviour, IAnimationEventReceiver
{
    public EntityAnimatorManager entityAnimator;
    public NavMeshAgent agent;
    public float stunDuration;

    public AttackData firstAttack; // Base attack (charge) from which branches out all other attacks
    public AttackData currentAttack;
    public IGrabbable grabbedTarget;
    public Transform enemyTransform;
    public Transform playerTransform;
    public float detectionRadius = 10f;

    // getters and setters
    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public float chargeMultiplier;
    public HitboxManager hitbox; // for attacks and grab

    [Header("Context variables")]
    [SerializeField] EnemyBaseState _currentState;
    [SerializeField] EnemyStateFactory _states;

    // Setting the starting state for a Enemy
    private void Awake()
    {
        _states = new EnemyStateFactory(this); // adding state factory for this context (Enemy state manager)
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
    /// For activating the hitbox from outside by Animation Events. (FSM Function)
    /// </summary>
    public void OnHitboxActivate()
    {
        if (_currentState is EnemyAttackReleaseState attackState)
        {
            attackState.ActivateHitboxExternal();
        }
    }
    /// <summary>
    /// For deactivating the hitbox from outside by Animation Events. (FSM Function)
    /// </summary>
    public void OnHitboxDeactivate()
    {
        if (_currentState is EnemyAttackReleaseState attackState)
        {
            attackState.DeactivateHitboxExternal();
        }
    }
    /// <summary>
    /// For entering the recovery state from outside call by Animation Events. (FSM Function)
    /// </summary>
    public void OnRecoveryEnable()
    {
        if (_currentState is EnemyAttackReleaseState attackState)
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
        if (_currentState is EnemyAttackRecoveryState recoveryState)
        {
            Debug.Log("Disable recovery state called from animation event!");
            recoveryState.OnRecoveryDisableExternal();
        }
    }
    /// <summary>
    /// Here, we can check if we can grab someone
    /// </summary>
    public void OnCheckGrab()
    {
        if (_currentState is EnemyGrabState grabState)
        {
            grabState.GrabCheckExternal();
        }
    }

    /// <summary>
    /// When we use this function we will leave grab state
    /// </summary>
    public void OnGrabDisable()
    {
        if (_currentState is EnemyGrabState grabState)
        {
            grabState.GrabDisableExternal();
        }
    }

    public void OnHurtEvent()
    {
        _currentState.HandleHurtEvent();
    }
}
