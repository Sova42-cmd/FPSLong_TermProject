using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
public enum MovementState { walking, crouching, dashing, air }
public MovementState state;

[Header("===== Movement =====")]
public float walkSpeed;
private float moveSpeed;

[Header("===== Crouch =====")]
public float crouchSpeed;
private bool isCrouching = false;
private float standingHeight;
public KeyCode crouchKey = KeyCode.C;

[Header("==== Dash ====")]
public KeyCode dashKey = KeyCode.LeftAlt;
public float dashSpeed = 20f;
public float dashDuration = 0.2f;
public float dashCooldown = 2f;

public bool isDashing = false;
public float dashTimer = 0f;
public float dashCooldownTimer = 0f;
public Vector3 dashDirection;

[Header("===== Jump & Gravity =====")]
public float jumpHeight = 3f;
public float gravity = -40f;

[Header("===== Ground Check =====")]
public Transform groundCheck;
public float groundDistance = 0.4f;
public LayerMask groundMask;

// private state
private CharacterController controller;
private Vector3 velocity;
public bool isGrounded;

void Start()
{
    controller = GetComponent<CharacterController>();   
    standingHeight = controller.height;
    //controller.center = new Vector3(0f, standingHeight / 2f, 0f);
}

void Update()
{
    StateHandler();
    HandleMovement();
    HandleJump();
    HandleCrouch();
    //HandleDash();
}

void OnDrawGizmosSelected() // krasnenkiy ground check
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
}

private void StateHandler()
{
    if (isDashing)
    {
        state = MovementState.dashing;
        return;
    }

    if (Input.GetKey(crouchKey))
    {
        state = MovementState.crouching;
        moveSpeed = crouchSpeed;    
    }

    else if (isGrounded)
    {
        state = MovementState.walking;
        moveSpeed = walkSpeed;
    }

    else
    {
        state = MovementState.air;
    }
}

private void HandleJump()
{
    //ground check
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    //check for jump
    if (Input.GetButton("Jump") && isGrounded)
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -1.9f * gravity);
    }

    //falling down
    velocity.y += gravity * Time.deltaTime;

    controller.Move(velocity * Time.deltaTime);
}
    
private void HandleMovement()
{
    float x = Input.GetAxis("Horizontal");//Input with CAPITCAL I
    float z = Input.GetAxis("Vertical");

    //create moving vector (right = redAxis & forward = blueAxis)
    Vector3 move = transform.right * x + transform.forward * z;
    controller.Move(move * moveSpeed * Time.deltaTime);
}
private void HandleCrouch()
{
    if (Input.GetKeyDown(crouchKey))
    {
        controller.height = 0.05f;
    }

    if (Input.GetKeyUp(crouchKey))
    {
        controller.height = standingHeight;
    }
}
private void HandleDash()
{
    if (dashCooldownTimer > 0f)
        dashCooldownTimer -= Time.deltaTime;

    if (Input.GetKeyDown(dashKey) && !isDashing && dashCooldownTimer <= 0f)
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 inputDir = transform.right * x + transform.forward * z;
        dashDirection = inputDir.magnitude > 0.1f ? inputDir.normalized : transform.forward;
    }

    if (isDashing)
    {
        dashTimer -= Time.deltaTime;
        controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        if (dashTimer <= 0f)
        isDashing = false;
    }
}
}
