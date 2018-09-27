using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> MenuSceneButtons = new List<GameObject>();

    private int selected_option = 0;

    private float time_since_option_change = 0;

    void Start()
    {
        MenuSceneButtons[selected_option].GetComponent<Image>().color = Color.red;
    }

    void Update()
    {
        time_since_option_change += Time.deltaTime;

        if (Input.GetButtonDown("A_Player1"))
        {
            //Debug.Log("Player1 A");
            MenuSceneButtons[selected_option].GetComponent<Button>().onClick.Invoke();
        }

        if (Input.GetButtonDown("B_Player1"))
        {
            //Debug.Log("Player1 B");
        }

        if (Input.GetButtonDown("A_Player2"))
        {
            //Debug.Log("Player2 A");
        }

        if (Input.GetButtonDown("B_Player2"))
        {
            //Debug.Log("Player2 B");
        }

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
}
