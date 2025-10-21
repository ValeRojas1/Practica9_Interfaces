using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 4.5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Mira (mouse)")]
    [SerializeField] private Transform cameraPivot; // referencia a la Main Camera
    [SerializeField] private float mouseSensitivity = 0.12f; // ajusta a gusto
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private CharacterController _cc;
    private PlayerInputActions _input;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Vector3 _velocity;
    private float _pitch; // rotación vertical acumulada

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _input = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled  += ctx => _moveInput = Vector2.zero;

        _input.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _input.Player.Look.canceled  += ctx => _lookInput = Vector2.zero;
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Look();
        Move();
    }

    private void Look()
    {
        // Yaw: rotación horizontal del cuerpo
        transform.Rotate(Vector3.up * (_lookInput.x * mouseSensitivity));

        // Pitch: rotación vertical (cámara)
        _pitch -= _lookInput.y * mouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
        if (cameraPivot != null)
            cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    private void Move()
    {
        // Dirección local (frente/derecha del jugador)
        Vector3 move = (transform.forward * _moveInput.y + transform.right * _moveInput.x) * moveSpeed;

        // Aplicar gravedad
        if (_cc.isGrounded && _velocity.y < 0) _velocity.y = -2f;
        _velocity.y += gravity * Time.deltaTime;

        _cc.Move((move + _velocity) * Time.deltaTime);
    }
}
