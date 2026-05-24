using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    public Transform playerBody;
    public float mouseSensetivity = 700f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse inputs 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // only X rotation on the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // only Y rotation on the player root
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
