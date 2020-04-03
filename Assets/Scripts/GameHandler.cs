using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    private double highestAlly = 0;
    private GameObject winScreen;
    private GameObject loseScreen;
    private GameObject[] allyUnits;
    private GameObject[] enemyUnits;
    private bool allyDeadCheck;
    private bool enemyDeadCheck;
    private bool gameEnd = false;
    private bool takeTime = true;
    
    // Start is called before the first frame update
    void Start()
    {
        winScreen = GameObject.Find("Win_Canvas").transform.GetChild(0).GetChild(0).gameObject;
        loseScreen = GameObject.Find("Lose_Canvas").transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd) {
            allyDeadCheck = true;
            enemyDeadCheck = true;

            allyUnits = GameObject.FindGameObjectsWithTag("Ally");
            enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
            double currentallies = 0;
            foreach (GameObject go in allyUnits) {
                if (go.activeSelf) {
                    allyDeadCheck = false;
                    currentallies++;
                }
            }
            if (currentallies > highestAlly)
            {
                highestAlly = currentallies;
            }

            foreach (GameObject go in enemyUnits) {
                if (go.activeSelf) {
                    enemyDeadCheck = false;
                }
            }

            if (allyDeadCheck) {
                //gameEnd = true;
                if (!takeTime) {
                    Time.timeScale = 0.0f;
                    loseScreen.SetActive(true);
                }else {
                    StartCoroutine(DelayTime());
                } 
            }

            if (enemyDeadCheck) {
                //gameEnd = true;
                if (!takeTime) {
                    Time.timeScale = 0.0f;
                    winScreen.SetActive(true);
                    double finalscore = (currentallies / highestAlly) * 10;
                    string toChange = "Your Score: " + finalscore + "\n";
                    GameObject.Find("ScoreText").GetComponentInChildren<Text>().text = toChange;

                }
                else {
                    StartCoroutine(DelayTime());
                }   
            }  
        }
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(3);
        takeTime = false;
    }
    
}
