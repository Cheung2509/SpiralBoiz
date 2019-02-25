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
        //BUTTONS
        if (Input.GetButtonDown("A_Player" + player_no))
        {
            Debug.Log("Player" + player_no + "A");

        }

        if (Input.GetButtonDown("B_Player" + player_no))
        {
            Debug.Log("Player" + player_no + " B");
        }


        //LEFT AND RIGHT TRIGGERS
        if (Input.GetAxis("L_Trigger_Player" + player_no) > 0)
        {
            Vector2 force = (transform.right * speed)/3;


            rb2d.AddForce(-force);
        }
        if (Input.GetAxis("R_Trigger_Player" + player_no) > 0)
        {
            Vector2 force = transform.right * speed;

            Debug.Log("Player no :" + player_no);

            rb2d.AddForce(force);
        }


        //UP DOWN JOYSTICK
        if (Input.GetAxis("Vertical_Player" + player_no) > 0)
        {
            Debug.Log("Player" + player_no + "Down");

        }
        if (Input.GetAxis("Vertical_Player" + player_no) < 0)
        {
            Debug.Log("Player" + player_no + "Up");
        }


        //LEFT RIGHT JOYSTICK
        if (Input.GetAxis("Horizontal_Player" + player_no) > 0)
        {
            Debug.Log("Player" + player_no + "Right");

            Rotate();

        }
        if (Input.GetAxis("Horizontal_Player" + player_no) < 0)
        {
            Debug.Log("Player" + player_no + "Left");

            Rotate();
        }


        //Rotate();

        
	}

        
    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, Input.GetAxis("Horizontal_Player" + player_no) * rotationSpeed));
    }

    public void Explode(float radius, float power, Vector3 explosionPos)
    {
        Vector2 dir = transform.position - explosionPos;

        rb2d.AddForce(dir.normalized * power, ForceMode2D.Impulse);
    }
    //done
}
