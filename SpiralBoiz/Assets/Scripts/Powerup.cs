using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    bool collectable = true;
    float timer;
    [SerializeField]
    float reset_time = 3;

    AudioSource pickUpSound;

    private void Start()
    {
        pickUpSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (collectable == false)
        {
            timer += Time.deltaTime;

            if(timer > reset_time)
            {
                timer = 0;
                collectable = true;
                gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);
                //transform.GetChild(0).gameObject.SetActive(true);
            }
        }


    }

    void OnTriggerEnter2D(Collider2D candidate)
    {
        if (collectable == true)
        {
            if (candidate.gameObject.tag == "Player")
            {
                candidate.gameObject.GetComponent<CarController>().addBoostResource(15);
                collectable = false;
                gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
                //this.transform.GetChild(0).gameObject.SetActive(false);
                pickUpSound.Play(0);
            }
        }
    }
}
