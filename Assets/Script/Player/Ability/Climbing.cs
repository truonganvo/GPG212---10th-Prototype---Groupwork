using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // The speed at which the player moves in the Y position
    [SerializeField] private Rigidbody rb;

    private KeyCode moveKey = KeyCode.W; // The key that the player must press or hold to move in the Y position
    private bool isMoving; // Whether or not the player is currently moving in the Y position

    [SerializeField] GameObject canvas;
    public bool isWallClimbOn = false;

    private void Start()
    {
        rb.isKinematic = false;
    }
    private void Update()
    {
        if (isWallClimbOn)
        {
            canvas.SetActive(true);
            Invoke("CanvasDisable", 5f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            if(isWallClimbOn)
            {
                // Check if the move key is pressed or held down
                if (Input.GetKey(moveKey))
                {
                    // Start moving the player in the Y position
                    isMoving = true;
                    rb.isKinematic = true;
                }
                else
                {
                    // Stop moving the player in the Y position
                    isMoving = false;
                    rb.isKinematic = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isMoving = false;
            rb.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            // Move the player in the Y position
            Vector3 moveVector = new Vector3(0f, moveSpeed * Time.fixedDeltaTime, 0f);
            transform.position += moveVector;
        }
    }

    private void CanvasDisable()
    {
        Destroy(canvas);
    }
}
