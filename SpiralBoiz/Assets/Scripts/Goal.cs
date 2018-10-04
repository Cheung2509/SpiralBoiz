using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Goal : MonoBehaviour
{
    BoxCollider2D boxCollider;

    private int count = 0;
    public Text scoreText;
    public Text hasScoredText;

    [SerializeField]
    private String scored_colour_name;

    [SerializeField]
    private Color ball_explosion_colour;

    private float reset_time = 3.0f;

	// Use this for initialization
	void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = count.ToString();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            //Increment score here
            Debug.Log("GOAL!!!!!");

            //Goal explosion
            ParticleSystem ball_ps = collision.GetComponent<ParticleSystem>();
            var main = ball_ps.main;
            main.startColor = ball_explosion_colour;

            collision.GetComponent<ParticleSystem>().Play();

            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<SpriteRenderer>().color = Color.clear;

            //stops it colliding with player after goal scored
            collision.GetComponent<CircleCollider2D>().isTrigger = true;

            Camera.main.gameObject.GetComponent<ScreenShake>().CameraShake(0.4f);

            StartCoroutine(GoalScored(collision.gameObject));

            count++;
        }
    }

    IEnumerator GoalScored(GameObject ball)
    {
        hasScoredText.text = scored_colour_name + " HAS SCORED";
        hasScoredText.color = ball_explosion_colour;

        yield return new WaitForSeconds(reset_time);
        hasScoredText.color = Color.clear;

        ball.transform.position = Vector3.zero;
        ball.GetComponent<SpriteRenderer>().color = Color.white;
        ball.GetComponent<CircleCollider2D>().isTrigger = false;
    }
}
