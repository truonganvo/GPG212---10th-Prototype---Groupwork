using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed, coefficienct;
    public LayerMask whatIsGround;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SurfaceAllign();
    }

    private void FixedUpdate()
    {
        MovementController();
        CameraEdit();
    }

    private void MovementController()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        Vector3 counterMovement = new Vector3(-rb.velocity.x, 0, -rb.velocity.z);

        rb.AddForce(movement * speed);
        rb.AddForce(counterMovement * coefficienct);
    }

    private void SurfaceAllign()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        if (Physics.Raycast(ray, out info, whatIsGround))
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, info.normal);
        }
    }

    private void CameraEdit()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Flatten the direction vectors to ignore the camera's tilt
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement vector based on input and camera direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = (horizontal * cameraRight + vertical * cameraForward).normalized * speed;

        // Move the player using the Rigidbody
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

}
