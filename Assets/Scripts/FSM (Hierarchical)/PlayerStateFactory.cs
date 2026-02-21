public class PlayerStateFactory
{
    PlayerStateManager _context;

    /// <summary>
    /// Constructor function. Called when we create a new instance of a class
    /// </summary>
    public PlayerStateFactory(PlayerStateManager currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle() 
    {
        return new PlayerFreeState(_context, this);
    }
    public PlayerBaseState Walk() 
    {
        return new PlayerMoveState(_context, this);
    }
    public PlayerBaseState Run() 
    {
        return new PlayerRunState(_context, this);
    }
    public PlayerBaseState AttackCharge()
    {
        return new PlayerAttackChargeState(_context, this);
    }
    public PlayerBaseState AttackRelease()
    {
        return new PlayerAttackReleaseState(_context, this);
    }
    public PlayerBaseState AttackRecovery()
    {
        return new PlayerAttackRecoveryState(_context, this);
    }
    public PlayerBaseState Stunned() 
    {
        return new PlayerStunnedState(_context, this);
    }
    public PlayerBaseState Grab() 
    {
        return new PlayerGrabState(_context, this);
    }
    public PlayerBaseState Grabbed() 
    {
        // TODO: Add Grabbed State to the State Machine
        return new PlayerGrabbedState(_context, this);
    }
}
