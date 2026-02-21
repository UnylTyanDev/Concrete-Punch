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
    public bool IsPressed; // Checks if the button is pressed or released
    public Vector2 MoveVector; // for Move

    public static Intent Move(Vector2 v) => new Intent { Type = IntentType.Move, MoveVector = v };
    public static Intent Attack(bool pressed) => new Intent { Type = IntentType.Attack, IsPressed = pressed };
    public static Intent Grab() => new Intent { Type = IntentType.Grab };
    public static Intent Jump() => new Intent { Type = IntentType.Jump };
}
