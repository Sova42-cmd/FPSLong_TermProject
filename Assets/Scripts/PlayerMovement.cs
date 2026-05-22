using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -12.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Debug.Log("isGrounded: " + isGrounded);

        //reser default velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");//Input with CAPITCAL I
        float z = Input.GetAxis("Vertical");

        //create moving vector (right = redAxis & forward = blueAxis)
        Vector3 move = transform.right * x +transform.forward * z; 

        //moving the player
        controller.Move(move * speed * Time.deltaTime);

        //check for jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //falling down
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
        } 
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }

    void OnDrawGizmosSelected() // krasnenkiy ground check
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

}
