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

    // Функція котра виконується при вході в стан
    public abstract void EnterState();

    // Функція котра обробляє кожну секунду функціонал певного стану
    public abstract void UpdateState();

    // Функція яка виконується при виході зі стану (Cleanup)
    public abstract void ExitState();

    public abstract void InitializeSubState();

    // Обробляє наміри сутності які були викликані з менеджеру станів
    public virtual void HandleIntent(PlayerStateManager entity, Intent intent) { }

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    public void ExitStates()
    {
        ExitState();
        if (_currentSubState != null)
        {
            _currentSubState?.ExitStates();
        }
    }
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
