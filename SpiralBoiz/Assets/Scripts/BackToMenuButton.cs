using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuButton : MonoBehaviour
{
    public void ButtonPressed()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ChangeScene(0);
    }
}
