using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotationSpeed = 12f;
    [SerializeField] Transform cameraTransform;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundedForce = -2f;

    Vector2 moveInput;
    float verticalVelocity;

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
        moveInput = Vector2.zero;
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = Vector2.ClampMagnitude(input, 1f);
    }

    void ApplyMovement()
    {
        // Проверка земли
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = groundedForce;
        }

        // Применяем гравитацию
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camRight * moveInput.x + camForward * moveInput.y;

        Vector3 finalMove = moveDir * moveSpeed;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);

        // Поворот только если есть движение
        if (moveDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}