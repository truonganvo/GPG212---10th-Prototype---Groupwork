using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    public Transform orientation;
    private Rigidbody rb;
    private Movement movementScript;

    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    public float dashCooldown;
    [SerializeField] float dashCdTimer;

    public KeyCode dashKey = KeyCode.E;

    [Header("Setting")]
    public bool useCameraFoward = true;
    public bool allowAllDirection = true;
    public bool resetVelocity = true;

    [Header("Setting")]
    public bool isDashingOn = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementScript= rb.GetComponent<Movement>();
    }

    private void Update()
    {
        if (isDashingOn)
        {
            if (Input.GetKeyUp(dashKey))
            {
                Dash();
            }
            if (dashCdTimer > 0)
            {
                dashCdTimer -= Time.deltaTime;
            }
        }
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCooldown;

        Vector3 dashDirection = Camera.main.transform.forward; // Get the direction that the camera is facing
        rb.MovePosition(rb.position + dashDirection.normalized * dashForce * Time.fixedDeltaTime); // Move the player using the Rigidbody
    }

    //This function allow the player to dash toward a direction based on the direction they moving with the key bind
    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirection)
        {
            direction = forwardT.forward * vertical + forwardT.right * horizontal;
        }
        else
        {
            direction = forwardT.forward;
        }
        if (vertical == 0 && horizontal == 0)
        {
            direction = forwardT.forward;
        }
        return direction.normalized ;
    }
}
