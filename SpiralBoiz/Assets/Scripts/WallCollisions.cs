using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisions : MonoBehaviour
{
    public float stepPercent = 50.0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // get rigidbody
        Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
        // get velocity
        Vector2 carVelocity = rb.velocity;

        // reflect velocity
        Vector2 reflectedVelocity = Vector2.Reflect(carVelocity, transform.up);

        // assign reflected velocity
        rb.velocity = reflectedVelocity/2;

        // rotate
        Quaternion rotation = Quaternion.FromToRotation(carVelocity, reflectedVelocity);
        rb.transform.rotation = rotation * rb.transform.rotation;       
    }
}
