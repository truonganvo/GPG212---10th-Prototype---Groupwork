using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("Player")]
    public Transform orientation;
    private Rigidbody rb;
    private Movement movementScript;

    [Header("Dashing")]
    [SerializeField] float dashForce;
    [SerializeField] float dashUpwardForce;
    [SerializeField] float dashDuration;

    public float dashCooldown;
    [SerializeField] float dashCdTimer;
    [SerializeField] TextMeshProUGUI dashCd;

    public KeyCode dashKey = KeyCode.E;

    [Header("Setting")]
    [SerializeField] bool useCameraFoward = true;
    [SerializeField] bool allowAllDirection = true;
    [SerializeField] bool resetVelocity = true;

    [Header("Check")]
    public bool isDashingOn = false;
    [SerializeField] GameObject canvas;


    //This is for fun, delete it if you want 
    [SerializeField] GameObject DashIcon;
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

            DashIcon.SetActive(true);
            canvas.SetActive(true);
            Invoke("CanvasDisable", 5f);
        }

        dashCd.text = dashCdTimer.ToString();
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

    private void CanvasDisable()
    {
        Destroy(canvas);
    }
}
