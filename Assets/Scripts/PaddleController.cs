using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Screen Bounds")]
    [SerializeField] private float minX = -7.5f; //left limit of the screen
    [SerializeField] private float maxX = 7.5f; //right limit of the screen

    private GameInputActions inputActions;
    private Rigidbody2D rb;
    private float _moveInput;

    private void Awake()
    {
        inputActions = new GameInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
    }
    void Update()
    {
        _moveInput = inputActions.Gameplay.Move.ReadValue<float>();  // read the movement input from the Input System
    }

    private void FixedUpdate()
    {
        float deltaX = _moveInput * moveSpeed * Time.fixedDeltaTime; // calculate the change in position based on input and speed

        float targetX = Mathf.Clamp(rb.position.x + deltaX, minX, maxX); // clamp the target position within the screen bounds

        Vector2 nextPosition = new Vector2(targetX, rb.position.y); // create the next position vector

        rb.MovePosition(nextPosition); // move the paddle to the next position
    }
}
