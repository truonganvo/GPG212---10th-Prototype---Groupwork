using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private Movement movementScript;

    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    public float dashCooldown;
    private float dashCdTimer;

    public KeyCode dashKey = KeyCode.E;

    [Header("Setting")]
    public bool useCameraFoward = true;
    public bool allowAllDirection = true;
    public bool resetVelocity = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementScript= rb.GetComponent<Movement>();
    }

    private void Update()
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

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCooldown;

        Transform forwardT;
        if (useCameraFoward)
        {
            forwardT = playerCam;
        }
        else
        {
            forwardT = orientation;
        }

        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);



        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {

    }

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
