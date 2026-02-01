using UnityEngine;
public enum IntentType
{
    None,
    Move,
    Attack,
    Grab,
    Jump,
    Cancel
}

public struct Intent
{
    public IntentType Type;
    public Vector2 MoveVector; // для Move

    public static Intent Move(Vector2 v) => new Intent { Type = IntentType.Move, MoveVector = v };
    public static Intent Attack() => new Intent { Type = IntentType.Attack };
    public static Intent Grab() => new Intent { Type = IntentType.Grab };
    public static Intent Jump() => new Intent { Type = IntentType.Jump };
}
