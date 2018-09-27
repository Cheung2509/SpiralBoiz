using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int no_players = 0;


	// Use this for initialization
	void Start ()
	{
	    DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
    }

    public void ChangeScene(int scene_index)
    {
        ChangeScene(scene_index);
    }

    public void QuitGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}
