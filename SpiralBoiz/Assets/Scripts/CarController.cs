using UnityEngine;

public class CarController : MonoBehaviour
{
    float speedForce = 5f;
    float torqueForce = -200f;
    float minimumRotationSpeed = 5;

    private Vector3 original_position;
    private Quaternion original_rotation;
    private Rigidbody2D rb2d;

    public bool no_input = true;

    public int player_no;

	void Start ()
	{
	    original_position = gameObject.transform.position;
	    original_rotation = gameObject.transform.rotation;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!no_input)
        {
            rb2d.velocity = ForwardVelocity();

            //if (Input.GetButton("Accelerate"))
            if (Input.GetAxis("R_Trigger_Player" + player_no) > 0 || Input.GetButton("Accelerate"))
            {
                rb2d.AddForce(transform.right * speedForce);
            }

            //if(Input.GetButton("Brake"))
            if (Input.GetAxis("L_Trigger_Player" + player_no) > 0 || Input.GetButton("Brake"))
            {
                rb2d.AddForce(transform.right * -speedForce / 2);
            }

            float tf = Mathf.Lerp(0, torqueForce, rb2d.velocity.magnitude / minimumRotationSpeed);

            if (transform.InverseTransformDirection(rb2d.velocity).x > 0)
            {
                rb2d.angularVelocity = Input.GetAxis("Horizontal_Player" + player_no) * tf;

            }
            else if (transform.InverseTransformDirection(rb2d.velocity).x < 0)
            {
                rb2d.angularVelocity = -(Input.GetAxis("Horizontal_Player" + player_no) * tf);
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                rb2d.angularVelocity = Input.GetAxis("Horizontal") * tf;
            }
        }
    }

    public void ResetCar()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.position = original_position;
        transform.rotation = original_rotation;

        no_input = true;
    }

    Vector2 ForwardVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }
    
    Vector2 SideVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    public void Explode(float power, Vector3 explosionPos)
    {
        Debug.Log("The Ball Explodeded!!!!!");
        Vector2 dir = transform.position - explosionPos;

        rb2d.velocity = Vector2.zero;
        rb2d.AddForceAtPosition(dir.normalized * power, explosionPos, ForceMode2D.Impulse);
    }
}
