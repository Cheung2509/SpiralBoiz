using UnityEngine;
using UnityEngine.Experimental.LowLevel;

public class CarController : MonoBehaviour
{
    float speedForce = 7f;
    float torqueForce = -200f;
    float minimumRotationSpeed = 2;

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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        if (!no_input)
        {
            rb.velocity = ForwardVelocity();

            //if (Input.GetButton("Accelerate"))
            if (Input.GetAxis("R_Trigger_Player" + player_no) > 0 || Input.GetButton("Accelerate"))
            {
                rb.AddForce(transform.right * ((Input.GetAxis("R_Trigger_Player" + player_no) * speedForce)));
            }

            //if(Input.GetButton("Brake"))
            if (Input.GetAxis("L_Trigger_Player" + player_no) > 0 || Input.GetButton("Brake"))
            {
                rb.AddForce(transform.right * -((Input.GetAxis("L_Trigger_Player" + player_no) * speedForce)) / 2);
            }

            float tf = Mathf.Lerp(0, torqueForce, rb2d.velocity.magnitude / minimumRotationSpeed);

            //if going forward
            if (transform.InverseTransformDirection(rb.velocity).x > 0)
            {
                rb2d.angularVelocity = Input.GetAxis("Horizontal_Player" + player_no) * tf;

                foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
                {
                    trail.emitting = true;
                }
            }
            //if going backwards
            else if (transform.InverseTransformDirection(rb.velocity).x < 0)
            {
                rb.angularVelocity = -(Input.GetAxis("Horizontal_Player" + player_no) * tf);
                foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
                {
                    trail.emitting = false;
                }
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                rb2d.angularVelocity = Input.GetAxis("Horizontal") * tf;
            }

            rb.angularVelocity *= 1.5f;
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
        Vector2 dir = transform.position - explosionPos;

        rb2d.velocity = Vector2.zero;
        rb2d.AddForceAtPosition(dir.normalized * power, explosionPos, ForceMode2D.Impulse);
    }
}
