using UnityEngine;
using UnityEngine.Experimental.LowLevel;

public class CarController : MonoBehaviour
{
    public float speedForce = 5.0f;
    public float maxSpeed = 7.0f;
    float torqueForce = -200.0f;
    public float minimumRotationSpeed = 2.0f;

    public GameObject rocketTrail;

    public GameObject trail1;
    public GameObject trail2;


    public GameObject PlayerNoUI;

    // Boost variables
    [Range(0, 100)] public float boost_resource = 25;
    int max_boost_resource = 100;

    [SerializeField]
    float boost_effect = 1.0f;

    [SerializeField]
    float boost_use_rate = 2f;

    private bool boosting = false;

    public float deceleration = 0.05f;

    private Vector3 original_position;
    private Quaternion original_rotation;
    private Rigidbody2D rb;

    public bool no_input = true;

    private bool inputting = false;

    public int player_no;

	void Start ()
	{
	    original_position = gameObject.transform.position;
	    original_rotation = gameObject.transform.rotation;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {        
        if (!no_input)
        {
            rb.velocity = ForwardVelocity();

            if (boosting)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed * 1.1f);
            }
            else
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }

            //if (Input.GetButton("Accelerate"))
            if (Input.GetAxis("R_Trigger_Player" + player_no) > 0 || Input.GetButton("Accelerate"))
            {
                rb.AddForce(transform.right * ((Input.GetAxis("R_Trigger_Player" + player_no) * speedForce)));
                if (transform.InverseTransformDirection(rb.velocity).x < 0)
                {
                    rb.AddForce(transform.right * ((Input.GetAxis("R_Trigger_Player" + player_no) * speedForce * 4)));
                }
            }

            //if(Input.GetButton("Brake"))
            if (Input.GetAxis("L_Trigger_Player" + player_no) > 0 || Input.GetButton("Brake"))
            {
                rb.AddForce(transform.right * -((Input.GetAxis("L_Trigger_Player" + player_no) * speedForce * 0.8f)));
                if (transform.InverseTransformDirection(rb.velocity).x > 0)
                {
                    rb.AddForce(transform.right * -((Input.GetAxis("L_Trigger_Player" + player_no) * speedForce * 3)));
                }
            }

            float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / minimumRotationSpeed);

            // car rotation
            // if above minimum speed for turning
            if (rb.velocity.magnitude > minimumRotationSpeed)
            {
                //if going forward
                if (transform.InverseTransformDirection(rb.velocity).x > 0)
                {
                    // turn car
                    rb.angularVelocity = Input.GetAxis("Horizontal_Player" + player_no) * tf;

                    // wheel trails
                    foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
                    {
                        trail.emitting = true;
                    }
                }
                //if going backwards
                else if (transform.InverseTransformDirection(rb.velocity).x < 0)
                {
                    // turn with inverted control (for people whio dont use inverted controls)
                    rb.angularVelocity = -(Input.GetAxis("Horizontal_Player" + player_no) * tf);

                    // add pizzazz
                    foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
                    {
                        trail.emitting = false;
                    }
                }
            }
            else
            {
                // if under minimum speed, stop turning altogether
                rb.angularVelocity = 0.0f;
            }

            // Car Boost
            if (Input.GetButton("A_Player" + player_no))
            {
                if (boost_resource > 0)
                {
                    //rb.AddForce(transform.right * (rb.velocity.normalized * boost_effect));
                    rb.AddForce(rb.velocity.normalized * boost_effect);
                    boost_resource -= ((boost_use_rate*10) * Time.deltaTime);

                    rocketTrail.GetComponent<TrailRenderer>().emitting = true;
                    boosting = true;
                }
                else
                {
                    rocketTrail.GetComponent<TrailRenderer>().emitting = false;
                    boosting = false;
                }
            }
            else
            {
                rocketTrail.GetComponent<TrailRenderer>().emitting = false;
                boosting = false;
            }


            // stupid keyboard turning controls
            if (Input.GetAxis("Horizontal") != 0)
            {
                rb.angularVelocity = Input.GetAxis("Horizontal") * tf;
            }

            // linear drag
            // if no triggers are held
            if (!IsATriggerPressed())
            {
                // slow linear motion
                rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, deceleration);
            }

            rb.angularVelocity *= 1.5f;
        }
    }

    private bool IsATriggerPressed()
    {
        if (Input.GetAxis("R_Trigger_Player" + player_no) == 0 && !Input.GetButton("Accelerate") &&
            Input.GetAxis("L_Trigger_Player" + player_no) == 0 && !Input.GetButton("Brake"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ResetCar()
    {
        trail1.SetActive(false);
        trail2.SetActive(false);

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

        rb.velocity = Vector2.zero;
        rb.AddForceAtPosition(dir.normalized * power, explosionPos, ForceMode2D.Impulse);
    }

    public void addBoostResource(int value)
    {
        boost_resource += value;

        if(boost_resource > max_boost_resource)
        {
            boost_resource = 100;
        }
    }
}
