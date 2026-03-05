/// <summary>
/// A hierarchical part of FSM. Its usefull because it passes the current context of a state to next state we about to switch
/// </summary>
public class EnemyStateFactory
{
    EnemyStateManager _context;

    /// <summary>
    /// Constructor function. Called when we create a new instance of a class
    /// </summary>
    public EnemyStateFactory(EnemyStateManager currentContext)
    {
        _context = currentContext;
    }

    public EnemyBaseState Idle()
    {
        return new EnemyFreeState(_context, this);
    }
    public EnemyBaseState Walk()
    {
        return new EnemyMoveState(_context, this);
    }
    public EnemyBaseState Run()
    {
        return new EnemyRunState(_context, this);
    }
    public EnemyBaseState AttackCharge()
    {
        return new EnemyAttackChargeState(_context, this);
    }
    public EnemyBaseState AttackRelease()
    {
        return new EnemyAttackReleaseState(_context, this);
    }
    public EnemyBaseState AttackRecovery()
    {
        return new EnemyAttackRecoveryState(_context, this);
    }
    public EnemyBaseState Stunned()
    {
        return new EnemyStunnedState(_context, this);
    }
    public EnemyBaseState Grab()
    {
        return new EnemyGrabState(_context, this);
    }
    public EnemyBaseState Grabbed()
    {
        return new EnemyGrabbedState(_context, this);
    }
}
