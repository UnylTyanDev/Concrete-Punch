/// <summary>
/// Direction from where attack came from (For hurt animations)
/// </summary>
public enum HitDirection
{
    Left,
    Right,
    Up,
    Down,
    Front,
    Back
}

public struct DamageData
{
    public float Amount;
    public HitDirection Direction;
    public float KnockbackForce;

    public DamageData(float amount, HitDirection direction, float knockbackForce)
    {
        Amount = amount;
        Direction = direction;
        KnockbackForce = knockbackForce;
    }
}
