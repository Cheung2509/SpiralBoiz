using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> MenuSceneButtons = new List<GameObject>();

    public int max_players = 2;

    [SerializeField]
    private List<GameObject> Players = new List<GameObject>();

    [SerializeField]
    private List<GameObject> JoinText = new List<GameObject>();

    private int selected_option = 0;

    private float time_since_option_change = 0;

    private List<bool> playerconnected = new List<bool>();

    void Start()
    {
        MenuSceneButtons[selected_option].GetComponent<Image>().color = Color.red;
        for (int i = 0; i < max_players; i++)
        {
            playerconnected.Add(false);
        }
    }

    void Update()
    {
        time_since_option_change += Time.deltaTime;

        if (Input.GetButtonDown("A_Player1"))
        {
            //Debug.Log("Player1 A");
            if (playerconnected[0] == true)
            {
                MenuSceneButtons[selected_option].GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                PlayerReady(0, true);
            }
        }

        if (Input.GetButtonDown("A_Player2"))
        {
            if (playerconnected[1] != true)
            {
                PlayerReady(1, true);
            }

            //Debug.Log("Player2 A");
        }


        if (Input.GetButtonDown("B_Player1"))
        {
            //Debug.Log("Player1 B");
        }
        
        if (Input.GetButtonDown("B_Player2"))
        {
            //Debug.Log("Player2 B");
        }


        //UP DOWN JOYSTICK
        if (Input.GetAxis("Vertical_Player1") > 0)
        {
            //Debug.Log("Player1 Down");
            changeMenuOption(false);

        }
        if (Input.GetAxis("Vertical_Player1") < 0)
        {
            //Debug.Log("Player1 Up");
            changeMenuOption(true);
        }

        if (Input.GetAxis("Vertical_Player2") > 0)
        {
            //Debug.Log("Player2 Down");
        }
        if (Input.GetAxis("Vertical_Player2") < 0)
        {
            //Debug.Log("Player2 Up");
        }
    }

    void changeMenuOption(bool up)
    {
        if (time_since_option_change > 0.2f)
        {
            time_since_option_change = 0;
            MenuSceneButtons[selected_option].GetComponent<Image>().color = Color.white;

            if (up && selected_option > 0)
            {
                selected_option--;
            }
            else if (!up && selected_option < MenuSceneButtons.Count-1)
            {
                selected_option++;
            }
            MenuSceneButtons[selected_option].GetComponent<Image>().color = Color.red;
        }
    }

    private void PlayerReady(int player, bool ready)
    {
        if (ready)
        {
            Players[player].GetComponent<Image>().color = Color.white;
        }
        else
        {
            Players[player].GetComponent<Image>().color = Color.clear;
        }

        playerconnected[player] = ready;
        JoinText[player].SetActive(ready);
    }
}
