using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> spawnPoints = new List<GameObject>();

    public PlayerScoreUpdater player_score_updater;
    bool saved_score;

    public float time_remaining = 300;
    public Text time_remaining_text;

    public float countdown_time = 3;
    public Text countdown_text;

    public bool game_playing = false;

    public GameObject redGoal;
    public GameObject blueGoal;

    public Text winText;

    private bool game_over = false;
    public GameObject gameOverMenuButton;

    private Color red = new Color(0.44f, 0, 0.08f);
    private Color blue = new Color(0.13f, 0.13f, 0.61f);

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
                    temp.GetComponent<SpriteRenderer>().color = blue;

                    TrailRenderer tr = temp.GetComponent<CarController>().rocketTrail.GetComponent<TrailRenderer>();

                    GradientColorKey[] colorKey = new GradientColorKey[3];
                    colorKey[0].color = blue;
                    colorKey[0].time = 0.0f;
                    colorKey[1].color = blue;
                    colorKey[1].time = 0.6f;
                    colorKey[2].color = blue;
                    colorKey[1].time = 1.0f;

                    GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];
                    alphaKey[0].alpha = 1.0f;
                    alphaKey[0].time = 0.0f;
                    alphaKey[1].alpha = 0.8f;
                    alphaKey[1].time = 0.6f;
                    alphaKey[2].alpha = 0.0f;
                    alphaKey[2].time = 1.0f;

                    Gradient gradient = new Gradient();
                    gradient.SetKeys(colorKey, alphaKey);

                    tr.colorGradient = gradient;
                }
                else //red
                {
                    temp.GetComponent<SpriteRenderer>().color = red;

                    TrailRenderer tr = temp.GetComponent<CarController>().rocketTrail.GetComponent<TrailRenderer>();

                    GradientColorKey[] colorKey = new GradientColorKey[3];
                    colorKey[0].color = red;
                    colorKey[0].time = 0.0f;
                    colorKey[1].color = red;
                    colorKey[1].time = 0.6f;
                    colorKey[2].color = red;
                    colorKey[1].time = 1.0f;

                    GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];
                    alphaKey[0].alpha = 1.0f;
                    alphaKey[0].time = 0.0f;
                    alphaKey[1].alpha = 0.8f;
                    alphaKey[1].time = 0.6f;
                    alphaKey[2].alpha = 0.0f;
                    alphaKey[2].time = 1.0f;

                    Gradient gradient = new Gradient();
                    gradient.SetKeys(colorKey, alphaKey);

                    tr.colorGradient = gradient;
                }

                temp.GetComponent<CarController>().PlayerNoUI.GetComponent<Text>().text = (i + 1).ToString();
            }
        }
    }

	// Update is called once per frame
	void Update ()
	{
	    if (game_over)
	    {
            if (!saved_score)
            {
                player_score_updater.saveNewScoreStats();
                saved_score = true;
            }

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
        countdown_text.color = Color.white;
        countdown_time = 3.0f;

        yield return new WaitForSeconds(3.0f);

        countdown_text.color = Color.clear;
        
        game_playing = true;
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Player"))
        {
            car.GetComponent<CarController>().no_input = false;

            car.GetComponent<CarController>().trail1.SetActive(true);
            car.GetComponent<CarController>().trail2.SetActive(true); 

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

        if (redGoal.GetComponent<Goal>().count > blueGoal.GetComponent<Goal>().count)
        {
            winText.color = red;
            winText.text = "WINNER\nRED\n" + 
                           redGoal.GetComponent<Goal>().count + "-" + blueGoal.GetComponent<Goal>().count;
        }
        else if(redGoal.GetComponent<Goal>().count < blueGoal.GetComponent<Goal>().count)
        {
            winText.color = blue;
            winText.text = "WINNER\nBLUE\n" +
                           blueGoal.GetComponent<Goal>().count + "-" + redGoal.GetComponent<Goal>().count;
        }
        else
        {
            winText.color = Color.white;
            winText.text = "DRAW\n" +
                blueGoal.GetComponent<Goal>().count + "-" + redGoal.GetComponent<Goal>().count;
        }
        gameOverMenuButton.SetActive(true);
    }
}
