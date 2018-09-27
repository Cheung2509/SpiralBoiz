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

        Vector2 force = transform.right * Input.GetAxis("Vertical") * speed;
       
        rb2d.AddForce(force);

	}


    private void rotate()
    {

        transform.Rotate(new Vector3(0, 0, Input.GetAxis("Horizontal") * rotationSpeed));
    }
}
