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
        switch (scene_index)
        {
            case 0:
                Debug.Log("Main Menu Called");
                break;
            case 1:
                Debug.Log("Start Game Called");
                break;
            case 2:
                Debug.Log("Settings Menu Called");
                break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
    }
}
