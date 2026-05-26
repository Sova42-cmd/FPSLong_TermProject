using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
public enum MovementState { walking, sprinting, crouching, air }
public MovementState state;

[Header("===== Movement =====")]
public float walkSpeed;
public float sprintSpeed;
public KeyCode sprintKey = KeyCode.LeftShift;
private float moveSpeed;

[Header("===== Crouch =====")]
public float crouchSpeed;
public float crouchYScale;
private float startYScale;
public KeyCode crouchKey = KeyCode.C;

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
private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();   
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

        StateHandler();
        HandleMovement();
        HandleJump();
        HandleCrouch();

    }

    void OnDrawGizmosSelected() // krasnenkiy ground check
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    private void StateHandler()
    {

        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;    
        }

        else if (isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
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
            Debug.Log("I'm crouching!! I'm crouching!!");
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }
}
