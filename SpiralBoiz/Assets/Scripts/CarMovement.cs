using UnityEngine;
using System.Collections;

public class CarPhysics : MonoBehaviour
{
    // car stats
    public float accelerationSpeed = 5.0f;         // speed percent multiplier
    public float topSpeed = 5.0f;                  // max speed multiplier
    public float steeringSpeed = 5.0f;             // turning speed multiplier
    public float minimumAngleDeceleration = 1.0f;  // decceleration (while facing 0 || 180 degrees)
    public float maximumAngleDeceleration = 5.0f;  // decceleration (while facing 90 || -90 degrees)

    // physics stats
    public float changeinMomemtumDirectionStepSize = 1.0f;
    public float changeinMomemtumValueStepSize = 1.0f;

    // car direction
    private Vector2 forwardDirection;              // direction car is facing
    private Vector2 idealMomentumDirection;        // direction car should be moving
    private Vector2 actualMomentumDirection;       // direction car is moving

    // car speed
    private float currentAccelerationSpeed = 0.0f; // acceleration speed (how long the up key has been pressed)
    public float idealMomentumValue = 0.0f;        // speed car should be moving
    public float actualMomentumValue = 0.0f;       // speed car is moving
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private float FloatMoveTowards(float currentValue, float targetvalue, float stepPercent)
    {
        float difference = 0.0f;

        if (currentValue > targetvalue)
        {
            difference -= (currentValue - targetvalue);
        }
        else
        {
            difference = (targetvalue - currentValue);
        }

        return currentValue - (difference * (100 / stepPercent));
    }

    private void CalculateIdealMomentumValue()
    {

    }

    void FixedUpdate()
    {
        // calculate idealMomentumDirection

        // calculate idealMomentumValue
        CalculateIdealMomentumValue();

        // if car is moving
        if (actualMomentumValue > 0)
        {
            // move momentumDirection closer to forwardDirection
            actualMomentumDirection = Vector2.MoveTowards(actualMomentumDirection, idealMomentumDirection, changeinMomemtumDirectionStepSize);

            // reduce momentumValue relative to difference between momentumDirection and forwardDirection
            actualMomentumValue = FloatMoveTowards(actualMomentumValue, idealMomentumValue, changeinMomemtumValueStepSize);
        }

        float v = Input.GetAxis("Vertical");
        // while using vertical input (acceleration)
        if (v != 0)
        {
            // increase momentumValue relative to accelerationSpeed / difference between forwardDirection and momentumDirection
        }

        float h = -Input.GetAxis("Horizontal");
        // while using horizontal input (steering)
        if (h != 0)
        {
            // alter angle of forwardDirection inversely relative to currentAccelerationSpeed
        }



        //void OldCode()
        //{
        //Vector2 forward = new Vector2(0.0f, 0.5f);
        //float steeringRightAngle;
        //if (rb.angularVelocity > 0)
        //{
        //    steeringRightAngle = -90;
        //}
        //else
        //{
        //    steeringRightAngle = 90;
        //}

        //Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
        //Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

        //float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

        //Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);

        //Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);

        //rb.AddForce(rb.GetRelativeVector(relativeForce));

        //}
        //OldCode();
    }
}