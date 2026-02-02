using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotationSpeed = 12f;
    [SerializeField] Transform cameraTransform;
    Vector2 moveInput;

    void Awake()
    {
        if (!cameraTransform && Camera.main)
            cameraTransform = Camera.main.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        ApplyMovement();
        moveInput = Vector2.zero; // 🔴 КЛЮЧЕВО: сброс каждый кадр
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = Vector2.ClampMagnitude(input, 1f);
    }

    void ApplyMovement()
    {
        if (moveInput.sqrMagnitude < 0.0001f)
            return;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camRight * moveInput.x + camForward * moveInput.y;

        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Поворот в сторону движения
        Quaternion targetRot = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }
}
