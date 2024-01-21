using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;  // The target point (center of the sphere)
    public float rotationSpeed {get; set;}   // Adjust the rotation speed as needed
    public float radius;  // Fixed radius from the target
    public float zoomSpeed;

    public bool isFocus;

    void Start()
    {
        // find the cube at the center of the world
        target = GameObject.Find("Cube").transform;

        // Set the initial position of the camera
        transform.position = target.position + new Vector3(0, 0, -10);
        transform.LookAt(target.position);

        this.rotationSpeed = 5.0f;
        // calculate distance from camera to target
        radius = Vector3.Distance(transform.position, target.position);

        this.isFocus = true;
        this.zoomSpeed = 100.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.isFocus = !this.isFocus;
        }
        if (isFocus){
            // Check for right mouse button click
            if (Input.GetMouseButton(1))
            {
                // Get mouse input
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // Rotate the camera around the target
                RotateCamera(mouseX, mouseY);
            }

            // print camera position

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Camera position: " + transform.position);
                Debug.Log("Camera point to: " + transform.forward);
            }
        }
        else {
            // free camera, with WASD to move up down left right, and mouse scroll to zoom in/out

            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * Time.deltaTime * 10;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * Time.deltaTime * 10;
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.position += transform.up * Time.deltaTime * 10;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.position -= transform.up * Time.deltaTime * 10;
            }

            // right mouse button to rotate
            if (Input.GetMouseButton(1))
            {
                // Get mouse input
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // Rotate the camera, not around anything
                transform.RotateAround(transform.position, Vector3.up, mouseX * rotationSpeed);
                transform.RotateAround(transform.position, transform.right, -mouseY * rotationSpeed);

            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * 10;
        }
        // Zoom In/Out with the mouse scroll wheel
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0) ZoomCamera(scrollWheel);

        this.radius = Vector3.Distance(transform.position, target.position);
    }

    void ZoomCamera(float zoomAmount)
    {
        transform.position += transform.forward * zoomAmount * zoomSpeed * Time.deltaTime;
    }

    void RotateCamera(float mouseX, float mouseY)
    {
        // Calculate the rotation based on mouse input
        float horizontalRotation = mouseX * rotationSpeed;
        float verticalRotation = -mouseY * rotationSpeed;  // Invert vertical rotation for natural movement

        // Apply rotation to the camera
        transform.RotateAround(target.position, Vector3.up, horizontalRotation);
        transform.RotateAround(target.position, transform.right, verticalRotation);

        // Update the camera's position to maintain a fixed radius
        Vector3 newPosition = (transform.position - target.position).normalized * radius + target.position;
        transform.position = newPosition;

        // Make the camera look at the target
        transform.LookAt(target.position);
    }
}
