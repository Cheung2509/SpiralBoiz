using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int goalID;

    BoxCollider2D boxCollider;

	// Use this for initialization
	void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            //Increment score here
            Debug.Log("GOAL!!!!!");
        }
    }
}
