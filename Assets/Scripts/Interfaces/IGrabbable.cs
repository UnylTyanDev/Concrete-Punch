using UnityEngine;

/// <summary>
/// Interface, that responsible for giving the ability to an object to be grabbed (by someone).
/// If class inherits this interface, thats means that this class provides certain functionality for combat grab state
/// </summary>
public interface IGrabbable
{
    bool CanBeGrabbed { get; }
    void OnGrabbed(Transform grabber);
    void OnRelease(); 
    Transform GetTransform();
}
