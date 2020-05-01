using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject uiController;
    private GameObject gameHandler;
    void Start()
    {
        uiController = GameObject.Find("UIController");
    	gameHandler = GameObject.Find("GameHandler");
    	string temp = "Welcome to the sandbox mode!";
        uiController.GetComponent<GameUI>().AppendLog(temp);
        temp = "The units that cost 0 are enemy units, create test battle scenarios between friendly and enemy units and have them engage with the Start Battle button.";
        uiController.GetComponent<GameUI>().AppendLog(temp);
        gameHandler.GetComponent<GameHandler>().ActivateTrainingRoom();
    }

}
