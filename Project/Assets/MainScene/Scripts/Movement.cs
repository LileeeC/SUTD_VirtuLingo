using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce = 35f;
    private bool isGrounded = true;

    public float movingSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.A)) rb.AddForce(-transform.right * movingSpeed);
        if (Input.GetKey(KeyCode.D)) rb.AddForce(transform.right * movingSpeed);
        if (Input.GetKey(KeyCode.W)) rb.AddForce(transform.forward * movingSpeed);
        if (Input.GetKey(KeyCode.S)) rb.AddForce(-transform.forward * movingSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = false;
        }
    }
}
