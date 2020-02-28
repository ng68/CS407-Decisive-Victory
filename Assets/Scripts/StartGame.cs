using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject startButton;

    public void MouseClick(string buttonType)
    {
        if (buttonType == "StartButton")
        {
            Time.timeScale = 1.0f;
            startButton.SetActive(false);
        }
    }
}
