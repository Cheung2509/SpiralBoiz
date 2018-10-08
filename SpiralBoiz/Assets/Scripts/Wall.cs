using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {


        // get car rigidbody
        Rigidbody2D carRB = collision.collider.gameObject.GetComponent<Rigidbody2D>();

        // get car velocity
        Vector2 carVelocity = carRB.velocity;

        // reflect velocity
        Vector2 reflect = Vector2.Reflect(carVelocity, collision.contacts[0].normal);
    }
}
