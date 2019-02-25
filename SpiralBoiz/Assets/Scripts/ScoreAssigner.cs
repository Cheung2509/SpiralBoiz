using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAssigner : MonoBehaviour {

    int current_player;

    private void OnCollisionEnter2D(Collision2D candidate)
    {
        if(candidate.gameObject.tag == "Player")
        {
            current_player = candidate.gameObject.GetComponent<CarController>().player_no;
        }
    }

    public int get_current_player()
    {
        return current_player;
    }
    
}
