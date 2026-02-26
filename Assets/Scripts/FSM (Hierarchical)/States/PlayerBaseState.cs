/// <summary>
/// An abstract class. The base of what the state should be like
/// </summary>
public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private PlayerStateManager _ctx; // context
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;

    protected bool IsRootState { set { _isRootState = value; }}
    protected PlayerStateManager Ctx { get { return _ctx; }}
    protected PlayerStateFactory Factory { get { return _factory; } }

    public PlayerBaseState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) 
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    /// <summary>
    /// Function that is executed when entering the state
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// Function that processes the state’s functionality every second
    /// </summary>
    public abstract void UpdateState();

    /// <summary>
    /// Function that is executed when exiting the state (Cleanup)
    /// </summary>
    public abstract void ExitState();

    public abstract void InitializeSubState();

    /// <summary>
    /// Handles entity intents that were triggered from the state manager
    /// </summary>
    /// <param name="entity">Context of FSM of current entity</param>
    /// <param name="intent">The player's desire to take an action (intent). Depens on what button is pressed</param>
    public virtual void HandleIntent(PlayerStateManager entity, Intent intent) { }

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    /// <summary>
    /// Function created for exiting all the states (including sub-states)
    /// </summary>
    public void ExitStates()
    {
        ExitState();
        if (_currentSubState != null)
        {
            _currentSubState?.ExitStates();
        }
    }

    /// <summary>
    /// A function that helps to switch states correctly. Entering the new, leaving the present
    /// </summary>
    /// <param name="newState"></param>
    protected void SwitchState(PlayerBaseState newState) 
    {
        // exit current state
        ExitState();

        newState.EnterState();

        // entering new state
        if (_isRootState)
        {
            // Switch current state of context
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            // Set the current super-state sub-state to the new state
            _currentSuperState.SetSubState(newState);
        }


    }
    // Adding superstate-state (parent) for states that we want happpening in the same time when another (sub-state) is currently running
    // Parent knows the child
    protected void SetSuperState(PlayerBaseState newSuperState) 
    {
        _currentSuperState = newSuperState;
    }
    // Adding sub-state (child) for states that we want happpening in the same time when another (super state) is currently running
    // Child knows the parent
    protected void SetSubState(PlayerBaseState newSubState) 
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
