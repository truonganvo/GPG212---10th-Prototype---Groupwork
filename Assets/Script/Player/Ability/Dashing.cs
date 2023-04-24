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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementScript= rb.GetComponent<Movement>();

        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(dashKey))
        {
            Dash();
        }
    }
    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCooldown;
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {

    }
}
