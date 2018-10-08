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
                int no_players = 0;
                foreach (bool player in playerconnected)
                {
                    if (player == true)
                    {
                        no_players++;
                        Debug.Log(no_players);
                    }

                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().no_players =
                        no_players;
                }
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
            MenuSceneButtons[selected_option].GetComponentInChildren<Text>().color = new Color(0.8f, 0.8f, 0.8f);

            if (up && selected_option > 0)
            {
                selected_option--;
            }
            else if (!up && selected_option < MenuSceneButtons.Count - 1)
            {
                selected_option++;
            }

            MenuSceneButtons[selected_option].GetComponentInChildren<Text>().color = Color.blue;
        }
    }

    private void PlayerReady(int player, bool ready)
    {
        if (ready)
        {
            Players[player].GetComponent<Image>().color += new Color(0, 0, 0, 1);
            MenuSceneButtons[selected_option].GetComponentInChildren<Text>().color = Color.blue;
        }
        else
        {
            Players[player].GetComponent<Image>().color -= new Color(0, 0, 0, 1);
        }

        playerconnected[player] = ready;
        JoinText[player].SetActive(!ready);
    }
}
