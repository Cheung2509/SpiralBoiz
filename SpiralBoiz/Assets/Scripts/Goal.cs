using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Goal : MonoBehaviour
{
    BoxCollider2D boxCollider;

    private int count = 0;
    public Text scoreText;

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
            count++;
        }
    }
}
