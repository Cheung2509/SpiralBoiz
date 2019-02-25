using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Goal : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public PlayerScoreUpdater player_score_updater;
    public ScoreAssigner score_assigner;

    public int count = 0;
    public Text scoreText;
    public Text hasScoredText;

    public float explosionPower;

    [SerializeField]
    private String scored_colour_name;

    [SerializeField]
    private Color ball_explosion_colour;

    public GameObject SceneController;

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
            // Player stats updated
            player_score_updater.updatePlayerScoreAmount(score_assigner.get_current_player());

            //Increment score here

            //Goal explosion
            ParticleSystem ball_ps = collision.GetComponent<ParticleSystem>();
            var main = ball_ps.main;
            main.startColor = ball_explosion_colour;

            collision.GetComponent<ParticleSystem>().Play();

            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().angularVelocity = 0;
            collision.GetComponent<SpriteRenderer>().color = Color.clear;

            //stops it colliding with player after goal scored
            collision.GetComponent<CircleCollider2D>().isTrigger = true;

            Camera.main.gameObject.GetComponent<ScreenShake>().CameraShake(0.4f);

            StartCoroutine(GoalScored(collision.gameObject));

            foreach (GameObject car in GameObject.FindGameObjectsWithTag("Player"))
            {
                car.GetComponent<CarController>().Explode(explosionPower, collision.transform.position);
            }

            count++;
        }
    }

    IEnumerator GoalScored(GameObject ball)
    {
        hasScoredText.text = scored_colour_name + " HAS SCORED!";
        hasScoredText.color = ball_explosion_colour;

        SceneController.GetComponent<GameSceneController>().game_playing = false;

        yield return new WaitForSeconds(reset_time);
        hasScoredText.color = Color.clear;

        ball.transform.position = Vector3.zero;
        ball.GetComponent<SpriteRenderer>().color = Color.white;
        ball.GetComponent<CircleCollider2D>().isTrigger = false;

        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Player"))
        {
            car.GetComponent<CarController>().ResetCar();
        }


        StartCoroutine(SceneController.GetComponent<GameSceneController>().CountdownToStart());

      
    }
}
