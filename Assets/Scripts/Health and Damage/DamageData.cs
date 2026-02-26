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

/// <summary>
/// A structure of collected data, created for transporting the data of a damage more compact
/// </summary>
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
