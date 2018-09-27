using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private float rotationInDegrees;

    private float yBoundary = 10.0f;
    private float xBoundary = 4.5f;

    private Rigidbody2D rb2d;

    private Sprite sprite;

    public int player_no;

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
        if (Input.GetButtonDown("A_Player1"))
        {
            //Debug.Log("Player1 A");
        }

        if (Input.GetButtonDown("A_Player2"))
        {

            //Debug.Log("Player2 A");
        }


        if (Input.GetButtonDown("B_Player1"))
        {
            //Debug.Log("Player1 B");
        }

        if (Input.GetButtonDown("B_Player2"))
        {
            //Debug.Log("Player2 B");
        }


        //UP DOWN JOYSTICK
        if (Input.GetAxis("Vertical_Player1") > 0)
        {
            //Debug.Log("Player1 Down");

        }
        if (Input.GetAxis("Vertical_Player1") < 0)
        {
            //Debug.Log("Player1 Up");
        }

        if (Input.GetAxis("Vertical_Player2") > 0)
        {
            //Debug.Log("Player2 Down");
        }
        if (Input.GetAxis("Vertical_Player2") < 0)
        {
            //Debug.Log("Player2 Up");
        }

        rotate();

        Vector2 force = transform.right * Input.GetAxis("Vertical") * speed;

        rb2d.AddForce(force);
	}


    private void rotate()
    {
        transform.Rotate(new Vector3(0, 0, Input.GetAxis("Horizontal") * rotationSpeed));
    }

    //done
}
