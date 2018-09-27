using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private float rotationInDegrees;

    private Rigidbody2D rb2d;

    private Sprite sprite;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<Sprite>();

        rotationInDegrees = transform.rotation.eulerAngles.z;

    }
	
	// Update is called once per frame
	void Update ()
    {
        rotate();

        Vector2 force = new Vector2(Input.GetAxis("Vertical") * transform.right.x * speed, 0);

        rb2d.AddForce(force);

	}


    private void rotate()
    {
        Quaternion rot = transform.rotation;

        rotationInDegrees = Input.GetAxis("Horizontal") * rotationSpeed;

        float newRotation = transform.rotation.z + rotationInDegrees;

        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, newRotation));
    }
}
