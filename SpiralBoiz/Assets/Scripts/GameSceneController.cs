using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    public float time_remaining = 300;
    public Text time_remaining_text;

    public float countdown_time = 3;
    public Text countdown_text;

    public bool game_playing = false;

    public GameObject orangeGoal;
    public GameObject blueGoal;

    public Text winText;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

	// Update is called once per frame
	void Update ()
	{
	    if (game_playing)
	    {
	        time_remaining -= Time.deltaTime;
	        time_remaining_text.text = Mathf.RoundToInt(time_remaining).ToString();
	        if (time_remaining <= 0)
	        {
	            EndGame();
	            game_playing = false;
	        }
	    }

	    else
	    {
	        countdown_time -= Time.deltaTime;
	        countdown_text.text = Mathf.RoundToInt(countdown_time).ToString();
        }
	}

    public IEnumerator CountdownToStart()
    {
        countdown_text.color = Color.yellow;
        countdown_time = 3.0f;

        yield return new WaitForSeconds(3.0f);

        countdown_text.color = Color.clear;
        
        game_playing = true;
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Player"))
        {
            car.GetComponent<CarController>().no_input = false;
        }
    }

    void EndGame()
    {
        if (orangeGoal.GetComponent<Goal>().count > blueGoal.GetComponent<Goal>().count)
        {
            winText.color = new Color(1.0f, 0.5f, 0);
            winText.text = "WINNER\nORANGE\n" + 
                           orangeGoal.GetComponent<Goal>().count + "-" + blueGoal.GetComponent<Goal>().count;
        }
        else if(orangeGoal.GetComponent<Goal>().count < blueGoal.GetComponent<Goal>().count)
        {
            winText.color = Color.blue;
            winText.text = "WINNER\nBLUE\n" +
                           blueGoal.GetComponent<Goal>().count + "-" + orangeGoal.GetComponent<Goal>().count;
        }
        else
        {
            //draw
        }
    }
}
