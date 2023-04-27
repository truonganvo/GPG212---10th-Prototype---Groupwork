using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] float jumpHeight = 20f;
    [SerializeField] bool grounded;

    [SerializeField] int maximumJumpCount = 2;
    [SerializeField] int remainJump = 0;

    [SerializeField] GameObject canvas;
    public bool isDoubleJumpOn = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //CHeck if isDoubleJumpOn boolean is true;
        if (isDoubleJumpOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) && remainJump > 0)
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                remainJump -= 1;
            }

            canvas.SetActive(true);
            Invoke("CanvasDisable", 5f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //When the character land on the ground & reset the jump count
        if(collision.gameObject.tag == "Ground")
        {
            grounded= true;
            remainJump = maximumJumpCount;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void CanvasDisable()
    {
        Destroy(canvas);
    }
}
