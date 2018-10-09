using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> spawnPoints = new List<GameObject>();

    public float time_remaining = 300;
    public Text time_remaining_text;

    public float countdown_time = 3;
    public Text countdown_text;

    public bool game_playing = false;

    public GameObject orangeGoal;
    public GameObject blueGoal;

    public Text winText;

    private bool game_over = false;
    public GameObject gameOverMenuButton;

    void Start()
    {
        StartCoroutine(CountdownToStart());

        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            for (int i = 0;
                i < GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().no_players;
                i++)
            {
                GameObject temp = Instantiate(player);
                temp.transform.position = spawnPoints[i].transform.position;
                temp.GetComponent<CarController>().player_no = i + 1;

                temp.transform.right = GameObject.FindGameObjectWithTag("Ball").transform.position - temp.transform.position;
                if (temp.transform.rotation.y != 0)
                {
                    temp.transform.rotation = new Quaternion(0, 0, temp.transform.rotation.y, 0);
                }

                if (i == 1 || i == 3)
                {
                    temp.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject temp = Instantiate(player);
                temp.transform.position = spawnPoints[i].transform.position;
                temp.GetComponent<CarController>().player_no = i + 1;

                temp.transform.right = GameObject.FindGameObjectWithTag("Ball").transform.position - temp.transform.position;
                if (temp.transform.rotation.y != 0)
                {
                    temp.transform.rotation = new Quaternion(0, 0, temp.transform.rotation.y, 0);
                }

                if (i == 1 || i == 3)
                {
                    temp.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
    }

	// Update is called once per frame
	void Update ()
	{
	    if (game_over)
	    {
	        if (Input.GetButtonDown("A_Player1"))
	        {
	            gameOverMenuButton.GetComponent<Button>().onClick.Invoke();
	        }
	    }

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
            car.GetComponent<CarController>().boost_resource = 25;
        }
    }

    void EndGame()
    {
        game_over = true;
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Player"))
        {
            car.GetComponent<CarController>().no_input = true;
        }
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;

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
        gameOverMenuButton.SetActive(true);
    }
}
