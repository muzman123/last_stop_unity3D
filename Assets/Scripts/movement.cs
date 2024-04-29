using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody rb;
    public Vector3 move;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    public float groundDrag;
 
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
 
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        SpeedControl();

        rb.drag = groundDrag;
    }
 
 
    void FixedUpdate()
    {
        moveCharacter(move);
    }
 
 
    private void moveCharacter(Vector3 direction)
    {
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
 
}
