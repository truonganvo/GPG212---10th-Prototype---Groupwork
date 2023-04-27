using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
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
        CharacterMovement();
    }

    //Making the player snap to surface terrain - not sure if its work or not, if not then have rigidbody on the player
    private void SurfaceAllign()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        if (Physics.Raycast(ray, out info, whatIsGround))
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, info.normal);
        }
    }

    //Make the character move at the direction that the camera face
    private void CharacterMovement()
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
