using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private Rigidbody rb;

    public float jumpHeight = 10f;
    public bool grounded;

    [SerializeField] int maximumJumpCount = 2;
    [SerializeField] int remainJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && remainJump > 0)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            remainJump -= 1;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
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
}
