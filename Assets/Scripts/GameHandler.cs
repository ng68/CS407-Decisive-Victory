using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    public GameObject winScreen;
    public GameObject loseScreen;

    private GameObject[] allyUnits;
    private GameObject[] enemyUnits;
    private bool allyDeadCheck;
    private bool enemyDeadCheck;
    private bool gameEnd = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd) {
            allyDeadCheck = true;
            enemyDeadCheck = true;

            allyUnits = GameObject.FindGameObjectsWithTag("Ally");
            enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject go in allyUnits) {
                if (go.active) {
                    allyDeadCheck = false;
                }
            }

            foreach (GameObject go in enemyUnits) {
                if (go.active) {
                    enemyDeadCheck = false;
                }
            }

            if (allyDeadCheck) {
                gameEnd = true;
                Time.timeScale = 0.0f;
                loseScreen.active = true;
            }

            if (enemyDeadCheck) {
                gameEnd = true;
                Time.timeScale = 0.0f;
                winScreen.active = true;
            }  
        }
    }
}
