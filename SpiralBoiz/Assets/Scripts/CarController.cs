using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float speedForce = 5f;
    float torqueForce = -200f;
    float minimumRotationSpeed = 5;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = ForwardVelocity();

        if (Input.GetButton("Accelerate"))
        {
            rb.AddForce(transform.right * speedForce);
        }

        if(Input.GetButton("Brake"))
        {
            rb.AddForce(transform.right * -speedForce * 2);
        }

        float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / minimumRotationSpeed);

        rb.angularVelocity = Input.GetAxis("Horizontal") * tf;
    }

    Vector2 ForwardVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    Vector2 SideVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

}
