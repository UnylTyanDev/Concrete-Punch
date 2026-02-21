using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Combat/AttackData")]
public class AttackData : ScriptableObject
{
    [Header("Attack Information")]
    public string animationName;
    public float chargeTime = 1f; // Max charge time
    public float baseDamage = 8f;
    public float damageMultiplierPerSecond = 4; // Amount of additional damage added per second when charging attack
    public HitDirection direction;
    public float knockbackForce = 1f;
    public float recoveryTime = 0.5f; // Amount of time before returning back to idle state
    public AttackData nextAttack;
}
