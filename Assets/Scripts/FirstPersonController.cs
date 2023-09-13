using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    private Rigidbody rb;
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
    }

    private void Update()
    {
        // Mouse look controls
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        // This ensures that the player can only rotate up and down to a certain extent, preventing a full loop.
        Camera cam = Camera.main;
        Vector3 camEulerAngles = cam.transform.localEulerAngles;
        float newRotationX = camEulerAngles.x - mouseY;
        if (newRotationX >= 80 && newRotationX <= 100) newRotationX = 80;  // Limit to prevent flipping over
        if (newRotationX <= 280 && newRotationX >= 260) newRotationX = 280;  // Same here
        cam.transform.localEulerAngles = new Vector3(newRotationX, camEulerAngles.y, camEulerAngles.z);

        // Movement controls
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = transform.right * moveX + transform.forward * moveZ;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }
}
